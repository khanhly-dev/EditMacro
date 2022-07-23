using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace EditMacro.Controllers
{
    [Route("[controller]")]
    public class HomeController : Controller
    {
        [HttpPost("edit-file")]
        public IActionResult EditMacroTextFile(string filePath)
        {
            try
            {
                var readStringArray = new List<string>();
                var writeStringArray = new List<string>();

                if (System.IO.File.Exists(filePath))
                {
                    var lines = System.IO.File.ReadAllLines(filePath);
                    for (int i = 0; i < lines.Length; i++)
                    {
                        readStringArray.Add(lines[i]);
                    }

                    if(readStringArray.Count > 0)
                    {
                        foreach (var item in readStringArray)
                        {
                            var lastIndex = item.Substring(item.Length - 1, 1);
                            var isNumber = Int32.TryParse(lastIndex, out int number);
                            if(isNumber)
                            {
                                writeStringArray.Add(item + ")");
                            }  
                            else
                            {
                                writeStringArray.Add(item);
                            }    
                        }
                    }

                    if(writeStringArray.Count > 0)
                    {
                        System.IO.File.WriteAllLines(filePath, writeStringArray);
                    }    
                }
                else
                {
                    return Ok("File does not exist");
                }
                return Ok(new { result = writeStringArray, status = "success" });
            }
            catch(Exception e)
            {
                throw e;
            }
        }
    }
}
