﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Assignment.Models;
using AutoMapper;
using Assignment.Models.DTO.Franchise;
using System.Net.Mime;

namespace Assignment.Controllers
{
    /// <summary>
    /// Controller for the Franchise
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    [ApiConventionType(typeof(DefaultApiConventions))]
    public class FranchisesController : ControllerBase
    {
        private readonly AssignmentDbContext _context;
        private readonly IMapper _mapper;
        /// <summary>
        /// Constructor with context and mapper
        /// </summary>
        /// <param name="context"></param>
        /// <param name="mapper"></param>
        public FranchisesController(AssignmentDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        /// <summary>
        /// Return a list of FranchiseReadDTO's
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FranchiseReadDTO>>> GetFranchises()
        {
            return _mapper.Map<List<FranchiseReadDTO>>(await _context.Franchises.ToListAsync());
        }

        /// <summary>
        /// Return franchise with id == {id}
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<FranchiseReadDTO>> GetFranchise(int id)
        {
            var franchise = await _context.Franchises.FindAsync(id);

            if (franchise == null)
            {
                return NotFound();
            }

            return _mapper.Map<FranchiseReadDTO>(franchise);
        }

        /// <summary>
        /// Return Franchise by name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        [HttpGet("{name}/byname")]
        public async Task<ActionResult<FranchiseReadDTO>> GetFranchiseByName(string name)
        {
            Franchise franchise;
            try
            {
                franchise = await _context.Franchises.Where(m => m.Name == name).FirstAsync();
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }

            
            return _mapper.Map<FranchiseReadDTO>(franchise);
        }

        /// <summary>
        /// Gets a selection of franchises.  Offset==how many you would like to skip.  Number==how many you want returned
        /// </summary>
        /// <param name="offset"></param>
        /// <param name="number"></param>
        /// <returns></returns>
        [HttpGet("{offset}/group")]
        public async Task<ActionResult<IEnumerable<FranchiseReadDTO>>> GetSomeFranchises(int offset, int number)
        {
            // return BadRequest if number is too low
            if (number < 1) return BadRequest("Number < 1");

            List<Franchise> franchises;
            // getting the requested character records
            try
            {
                franchises = await _context.Franchises
                .Skip(offset)
                .Take(number)
                .ToListAsync();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
            if (franchises.Count == 0)
            {
                return NotFound("No records found");
            }
            // Gets all FranchiseDTO's
            return _mapper.Map<List<FranchiseReadDTO>>(franchises);
        }

        /// <summary>
        /// Update franchise with id == {id}
        /// </summary>
        /// <param name="id"></param>
        /// <param name="franchise"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFranchise(int id, FranchiseReadDTO franchise)
        {
            if (id != franchise.Id)
            {
                return BadRequest();
            }

            Franchise rFran = _mapper.Map<Franchise>(franchise);

            _context.Entry(rFran).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FranchiseExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        /// <summary>
        /// Add/create a new franchise
        /// </summary>
        /// <param name="franchise"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<Franchise>> PostFranchise(FranchiseCreateDTO franchise)
        {
            // Make sure a name is added
            if (franchise.Name == "string") return BadRequest("Please add a name");

            // Test for duplicate name
            if (NameExistInDB(franchise)) return BadRequest("Name exist in DB");

            Franchise franchiseToAdd = _mapper.Map<Franchise>(franchise);
            _context.Franchises.Add(franchiseToAdd);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetFranchise", new { id = franchiseToAdd.Id }, franchise);
        }

        /// <summary>
        /// Delete franchise with ID == {id}
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFranchise(int id)
        {
            if (id == 1) return BadRequest("Can not remove the default/no franchise at ID 1"); 
            var franchise = await _context.Franchises.FindAsync(id);
            if (franchise == null)
            {
                return NotFound();
            }

            // Remove this franchise from all movies with it in, setting franchise ID to 1(which is "No known franchise or missing information")
           foreach(Movie movie in _context.Movies)
            {
                if(movie.FranchiseId == id)
                {
                    movie.FranchiseId = 1;
                }
            }

            _context.Franchises.Remove(franchise);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        /// <summary>
        /// Add a list (int) of Movie Ids to  a franchise id == {id}
        /// </summary>
        /// <param name="id"></param>
        /// <param name="movies"></param>
        /// <returns></returns>
        [HttpPut("{id}/movies")]
        public async Task<IActionResult> UpdateMoviesInFranchise(int id, List<int> movies)
        {
            if (!FranchiseExists(id))
            {
                return NotFound();
            }

            // Load the franchise with a collection of current movies
            Franchise franchiseToUpdateMovies = await _context.Franchises
                .Include(c => c.Movies)
                .Where(c => c.Id == id)
                .FirstAsync();

            // Loop through the list of movies
            foreach (int movId in movies)
            {
                // Try to find the movie
                Movie mov = await _context.Movies.FindAsync(movId);
                if (mov == null)
                    return BadRequest("Movie does not exist!");
                // If the franchise does not contain the movie, add it
                if(!franchiseToUpdateMovies.Movies.Contains(mov))
                {
                    franchiseToUpdateMovies.Movies.Add(mov);
                }
            }
            

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }

            return NoContent();
        }

        /// <summary>
        /// Test if any record has id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>bool</returns>
        private bool FranchiseExists(int id)
        {
            return _context.Franchises.Any(e => e.Id == id);
        }


        /// <summary>
        /// Tests if Name exist in db 
        /// </summary>
        /// <param name="character"></param>
        /// <returns>bool</returns>
        private bool NameExistInDB(FranchiseCreateDTO franchise)
        {
            return _context.Franchises.Any(e => e.Name == franchise.Name);
        }
    }
}
