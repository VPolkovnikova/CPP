using System.Collections.Immutable;
using Logics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApplication.Models.LaboratoryWork;

namespace WebApplication.Controllers;
using static Statics;
using IndexModel = Models.LaboratoryWork.IndexModel;


[Authorize]
public class LaboratoryWorkController : Controller
{
    public IActionResult Index(IndexModel.GetInput input)
    {
        if (ModelState.IsValid)
        {
            ValidateLaboratoryWork(input.LaboratoryWorkNumber);
        }
        return View(new IndexModel.Output
        {
            LaboratoryWorkNumber = input.LaboratoryWorkNumber
        });
    }


    [HttpPost]
    public IActionResult Index(IndexModel.PostInput input)
    {
        if (ModelState.IsValid)
        {
            ValidateLaboratoryWork(input.LaboratoryWorkNumber);
        }
        return View(ModelState.IsValid
            ? new IndexModel.Output
            {
                LaboratoryWorkNumber = input.LaboratoryWorkNumber,
                InputText = input.InputText,
                OutputText = LaboratoryWorkRunMethods[input.LaboratoryWorkNumber - 1](input.InputText ?? "")
            }
            : new IndexModel.Output
            {
                LaboratoryWorkNumber = input.LaboratoryWorkNumber
            }
        );
    }


    public IActionResult All() => View(new AllModel.Output
    {
        NumberLaboratoryWorks = LaboratoryWorkRunMethods.Length
    });


    private void ValidateLaboratoryWork(int number)
    {
        if (!(number > 0 && number <= LaboratoryWorkRunMethods.Length))
        {
            ModelState.AddModelError("", $"Laboratory work №{number} does not exist.");
        }
    }
}


file static class Statics
{
    public static ImmutableArray<Func<string, string>> LaboratoryWorkRunMethods { get; } =
    [
        LaboratoryWork1.Run,
        LaboratoryWork2.Run,
        LaboratoryWork3.Run
    ];
}