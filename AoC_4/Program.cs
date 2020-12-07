using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC_4
{
    class Program
    {
        static void Main(string[] args)
        {
            var lines = File.ReadAllLines("input.txt");
            var passport = new passport();
            var passports = new List<passport>();
            foreach(var line in lines)
            {
                if (String.IsNullOrWhiteSpace(line)) { passports.Add(passport); passport = new passport(); continue; }
                var parts = line.Split(' ');
                foreach(var part in parts)
                {
                    var thigns = part.Split(':');
                    switch(thigns[0])
                    {
                        case "byr":
                            passport.byr = thigns[1];
                            break;
                        case "iyr":
                            passport.iyr = thigns[1];
                            break;
                        case "eyr":
                            passport.eyr = thigns[1];
                            break;
                        case "hgt":
                            passport.hgt = thigns[1];
                            break;
                        case "hcl":
                            passport.hcl = thigns[1];
                            break;
                        case "ecl":
                            passport.ecl = thigns[1];
                            break;
                        case "pid":
                            passport.pid = thigns[1];
                            break;
                        case "cid":
                            passport.cid = thigns[1];
                            break;
                    }
                }
                
            }
            passports.Add(passport);
            int valid = 0;
            foreach(var tp in passports)
            {
                if (tp.isValid()) valid++;
            }

            Console.WriteLine(valid);

        }
    }

    public class passport
    {
        public string ecl { get; set; }
        public string pid { get; set; }
        public string byr { get; set; }
        public string iyr { get; set; }
        public string eyr { get; set; }
        public string hgt { get; set; }
        public string hcl { get; set; }
        public string cid { get; set; }

        public bool isValid()
        {
            if (!String.IsNullOrWhiteSpace(ecl) && !String.IsNullOrWhiteSpace(pid) && !String.IsNullOrWhiteSpace(byr) &&
                !String.IsNullOrWhiteSpace(hgt) && !String.IsNullOrWhiteSpace(eyr) && !String.IsNullOrWhiteSpace(iyr) &&
                 !String.IsNullOrWhiteSpace(hcl) && eclValid() && pidValid() && hclValid() && hgtValid() && eyrValid() && iyrValid() && byrValid())
                return true;
            else
                return false;


        }

        private bool byrValid()
        {
            int value;
            if(int.TryParse(byr, out value))
            {
                if (value < 1920 || value > 2002) return false;
            }

            return true;
        }

        private bool iyrValid()
        {
            int value;
            if (int.TryParse(iyr, out value))
            {
                if (value < 2010 || value > 2020) return false;
            }

            return true;
        }

        private bool eyrValid()
        {
            int value;
            if (int.TryParse(eyr, out value))
            {
                if (value < 2020 || value > 2030) return false;
            }

            return true;
        }

        private bool hgtValid()
        {
            if(hgt.EndsWith("cm"))
            {
                var newhgt = int.Parse(hgt.Replace("cm", ""));
                if(newhgt < 150 || newhgt > 193) { return false; }
                else
                {
                    return true;
                }
            }
            else if(hgt.EndsWith("in"))
            {
                var newhgt = int.Parse(hgt.Replace("in", ""));
                if (newhgt < 59 || newhgt > 76) { return false; }
                else
                {
                    return true;
                }
            }
            else
            {
                return false;
            }
        }

        private bool hclValid()
        {
            var parts = hcl.ToCharArray();
            if(parts.Length == 7 && parts[0] == '#')
            {
                for(int i = 1; i < 6; i++)
                {
                    char c = parts[i];
                    bool is_hex_char = (c >= '0' && c <= '9') ||
                   (c >= 'a' && c <= 'f') ||
                   (c >= 'A' && c <= 'F');
                    if (!is_hex_char) return false;
                }
                return true;
            }
            return false;
        }

        private bool pidValid()
        {
            int parts;
            if(int.TryParse(pid, out parts))
            {
                var blah = pid.ToCharArray();
                if(blah.Length == 9) { return true; }
            }
            return false;
        }

        private bool eclValid()
        {
            switch (ecl)
            {
                case "amb":
                case "blu":
                case "brn":
                case "gry":
                case "grn":
                case "hzl":
                case "oth":
                    return true;

            }
            return false;

        }
    }
}
