using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FirstDatabaseLab.DB.Schema;

namespace FirstDatabaseLab.Controllers;

[Route("[controller]")]
[ApiController]
public class CustomerController(MyTestDbContext _context) : ControllerBase
{
  [HttpPost("[action]")]
  public async Task<ActionResult<IEnumerable<Customer>>> Search()
  {
    return await _context.Customer.ToListAsync();
  }

  [HttpPost("[action]/{id}")]
  public async Task<ActionResult<Customer>> Read(int id)
  {
    var customer = await _context.Customer.FindAsync(id);

    if (customer == null)
    {
      return NotFound();
    }

    return customer;
  }

  [HttpPost("[action]/{id}")]
  public async Task<ActionResult<Customer>> Update(int id, Customer customer)
  {
    try
    {
      if (id != customer.Id)
        return BadRequest();

      using var txn = await _context.Database.BeginTransactionAsync();
      var info = await _context.Customer.FindAsync(id);
      if (info == null)
        return NotFound();

      // update
      info.Name = customer.Name;
      info.Email = customer.Email;
      // auto update system filed
      info.UpdatedAt = DateTimeOffset.Now;

      await _context.SaveChangesAsync(); // 將會更新 info
      await txn.CommitAsync();
      return info;
    }
    catch(Exception ex)
    {
      return UnprocessableEntity(new
      {
        ex.Message,
        ex.StackTrace
      });
    }
  }

  [HttpPost("[action]")]
  public async Task<ActionResult<Customer>> Create(Customer customer)
  {
    var now = DateTimeOffset.Now;
    customer.CreatedAt = now;
    customer.UpdatedAt = now;

    _context.Customer.Add(customer);
    await _context.SaveChangesAsync();

    return CreatedAtAction("Read", new { id = customer.Id }, customer);
  }

  [HttpPost("[action]/{id}")]
  public async Task<IActionResult> Delete(int id)
  {
    var customer = await _context.Customer.FindAsync(id);
    if (customer == null)
    {
      return NotFound();
    }

    _context.Customer.Remove(customer);
    await _context.SaveChangesAsync();

    return NoContent();
  }
}
