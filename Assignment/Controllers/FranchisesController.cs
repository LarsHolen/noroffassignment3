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
            var franchise = await _context.Franchises.FindAsync(id);
            if (franchise == null)
            {
                return NotFound();
            }

            _context.Franchises.Remove(franchise);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        /// <summary>
        /// Add a list (int) of Movie Ids to a franchise id == {id}
        /// </summary>
        /// <param name="id"></param>
        /// <param name="movies"></param>
        /// <returns></returns>
        [HttpPut("{id}/movies")]
        public async Task<IActionResult> UpdateCharactersInMovie(int id, List<int> movies)
        {
            if (!FranchiseExists(id))
            {
                return NotFound();
            }

            Franchise franchiseToUpdateMovies = await _context.Franchises
                .Include(c => c.Movies)
                .Where(c => c.Id == id)
                .FirstAsync();

            // TODOOOOOO:::::::::::
            // Trying to see if there is a nicer way of doing this, dont like the multiple calls
            List<Movie> movs = new();
            foreach (int movId in movies)
            {
                Movie mov = await _context.Movies.FindAsync(movId);
                if (mov == null)
                    return BadRequest("Movie does not exist!");
                movs.Add(mov);
            }
            franchiseToUpdateMovies.Movies = movs;

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



        private bool FranchiseExists(int id)
        {
            return _context.Franchises.Any(e => e.Id == id);
        }
    }
}
