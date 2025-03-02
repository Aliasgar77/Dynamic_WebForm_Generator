using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dynamic_WebForm_Generator.Models;
using Dynamic_WebForm_Generator.Data;
using Dynamic_WebForm_Generator.Models.DTOs;
using Newtonsoft.Json; // Your DbContext namespace

[Route("api/[controller]")]
[ApiController]
public class FormTemplateController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public FormTemplateController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: api/FormTemplate/templates
    #region("retrieve form templates")
    [HttpGet("templates")]
    public async Task<ActionResult<IEnumerable<FormTemplate>>> GetTemplates()
    {
        var templates = await _context.FormTemplates.ToListAsync();
        if (templates == null || templates.Count == 0)
        {
            return NotFound("No form templates found.");
        }
        return Ok(templates);
    }

    // GET: api/FormTemplate/template/1
    [HttpGet("template/{id}")]
    public async Task<IActionResult> GetTemplate(int id)
    {
        Console.WriteLine($"Fetching template for ID: {id}");

        var template = await _context.FormTemplates
            .FirstOrDefaultAsync(t => t.Id == id);

        if (template == null)
        {
            Console.WriteLine($"No template found for ID: {id}");
            return NotFound($"Template with ID {id} not found.");
        }

        Console.WriteLine($"Template found: {JsonConvert.SerializeObject(template)}");
        return Ok(template);
    }



    #endregion


    #region("save Form data")

    [HttpPost("SaveFormData")]
    public IActionResult SaveFormData([FromBody] FormData formData)
    {
        var existingForm = _context.FormData.FirstOrDefault(f => f.Id == formData.Id);

        if (existingForm != null)
        {
            existingForm.FormTemplate = formData.FormTemplate;
            _context.SaveChanges();
        }
        else
        {
            _context.FormData.Add(formData);
            _context.SaveChanges();
        }

        return Ok(new { message = "Form saved successfully!" });
    }


    #endregion



}
