using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Capstone.Classes
{
    public class FileIo
    {
        public string sourcefile = @"c:\store\inventory.csv";
        public string auditfile = @"c:\store\Log.txt";
        public List<Item> MakeInventory()
        {
            List<Item> items = new List<Item>();
                using (StreamReader sr = new StreamReader(sourcefile))
                {
                    while (!sr.EndOfStream)
                    {
                        string line = sr.ReadLine();
                        string[] splitString = line.Split("|");
                        Item item = new Item();
                        item.Id = (splitString[1]);
                        item.Name = splitString[2];
                        if (splitString[4] == "T")
                        {
                            item.IsWrapped = "Y";
                        }
                        else
                        {
                            item.IsWrapped = "N";
                        }      
                        item.TypeOfCandy = (splitString[0]);
                        item.Price = decimal.Parse(splitString[3]);
                        items.Add(item);
                    }
                }
            return items;
        }
        public void WritingAudit(string audit)
        {
            using(StreamWriter sw = new StreamWriter(auditfile,true))
            {
                sw.WriteLine(audit);

            }
        }

    }
}
