using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using UsersService.Data;
using UsersService.Models;

namespace UsersService.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class UsersController : ControllerBase
	{
		private readonly AppDbContext _context;

		public UsersController(AppDbContext context)
		{
			_context = context;
		}

		[HttpGet]
		public async Task<ActionResult<IEnumerable<User>>> GetUsers()
		{
			return await _context.Users.ToListAsync();
		}

		[HttpGet("{id}")]
		public async Task<ActionResult<User>> GetUser(Guid id)
		{
			var user = await _context.Users.FindAsync(id);

			return user == null ? NotFound() : Ok(user);
		}

		[HttpPost]
		public async Task<ActionResult<User>> CreateUser(User user)
		{
			try
			{
				_context.Users.Add(user);

				await _context.SaveChangesAsync();

				return CreatedAtAction(nameof(GetUser), new { id = user.Id }, user);
			}
			catch (DbUpdateException ex) when (ex.InnerException is PostgresException pgEx && pgEx.SqlState == "23505")
			{
				return Conflict(new { message = "Email already exists." });
			}
		}

		[HttpPut("{id}")]
		public async Task<ActionResult> UpdateUser(Guid id, User updatedUser)
		{
			if (id != updatedUser.Id) return BadRequest();

			_context.Entry(updatedUser).State = EntityState.Modified;

			await _context.SaveChangesAsync();

			return NoContent();
		}

		[HttpDelete("{id}")]
		public async Task<ActionResult> DeleteUser(Guid id)
		{
			var user = await _context.Users.FindAsync(id);

			if (user == null) return NotFound();

			_context.Users.Remove(user);

			await _context.SaveChangesAsync();

			return NoContent();
		}
	}
}