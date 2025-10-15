using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FirstDatabaseLab.DB.Data;
using FirstDatabaseLab.DB.Schema;

namespace FirstDatabaseLab.Controllers;

[Route("[controller]")]
[ApiController]
public class CustomerController(MyTestDbContext _context) : ControllerBase
{
  [HttpPost("[action]")]
  public async Task<ActionResult<IEnumerable<Customer>>> Search()
  {
    return await _context.Customers.ToListAsync();
  }

  [HttpPost("[action]/{id}")]
  public async Task<ActionResult<Customer>> Read(int id)
  {
    var customer = await _context.Customers.FindAsync(id);

    if (customer == null)
    {
      return NotFound();
    }

    return customer;
  }

  [HttpPost("[action]/{id}")]
  public async Task<IActionResult> Update(int id, Customer customer)
  {
    if (id != customer.Id)
    {
      return BadRequest();
    }

    _context.Entry(customer).State = EntityState.Modified;

    try
    {
      await _context.SaveChangesAsync();
    }
    catch (DbUpdateConcurrencyException)
    {
      if (!CustomerExists(id))
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

  [HttpPost("[action]")]
  public async Task<ActionResult<Customer>> Create(Customer customer)
  {
    _context.Customers.Add(customer);
    await _context.SaveChangesAsync();

    return CreatedAtAction("GetCustomer", new { id = customer.Id }, customer);
  }

  [HttpPost("[action]/{id}")]
  public async Task<IActionResult> Delete(int id)
  {
    var customer = await _context.Customers.FindAsync(id);
    if (customer == null)
    {
      return NotFound();
    }

    _context.Customers.Remove(customer);
    await _context.SaveChangesAsync();

    return NoContent();
  }

  private bool CustomerExists(int id)
  {
    return _context.Customers.Any(e => e.Id == id);
  }
}
