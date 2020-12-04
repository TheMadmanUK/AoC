using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode._2020
{
    public static class Day04
    {
        public static int RunPart1()
        {
            var passports = LoadPassports(File.ReadAllLines(@"2020\Input\Day04.txt"));
            return passports.Count(x => x.IsComplete);
        }

        public static int RunPart2()
        {
            var passports = LoadPassports(File.ReadAllLines(@"2020\Input\Day04.txt"));
            return passports.Count(x => x.IsValid);
        }

        private static List<Passport> LoadPassports(string[] details)
        {
            var passports = new List<Passport>();
            var passport = new Passport();

            foreach (var detailLine in details)
            {
                if (detailLine == string.Empty && passport.IsDirty)
                {
                    passports.Add(passport);
                    passport = new Passport();
                }

                passport.LoadDetailLine(detailLine);
            }

            if (passport.IsDirty) passports.Add(passport);

            return passports;
        }

        private class Passport
        {
            public string BirthYear { get; private set; }
            public bool IsBirthYearValid => !string.IsNullOrEmpty(BirthYear) && BirthYear.Length == 4 && int.TryParse(BirthYear, out int year) && 
                year >= 1920 && year <= 2002;

            public string IssueYear { get; private set; }
            public bool IsIssueYearValid => !string.IsNullOrEmpty(IssueYear) && IssueYear.Length == 4 && int.TryParse(IssueYear, out int year) && 
                year >= 2010 && year <= 2020;

            public string ExpirationYear { get; private set; }
            public bool IsExpirationYearValid => !string.IsNullOrEmpty(ExpirationYear) && ExpirationYear.Length == 4 && int.TryParse(ExpirationYear, out int year) &&
                year >= 2020 && year <= 2030;

            public string Height { get; private set; }
            public bool IsHeightValid { get
                {
                    if (string.IsNullOrEmpty(Height)) return false;

                    return (Height[^2..]) switch
                    {
                        "cm" => int.TryParse(Height[..^2], out int checkValue) && checkValue >= 150 && checkValue <= 193,
                        "in" => int.TryParse(Height[..^2], out int checkValue) && checkValue >= 59 && checkValue <= 76,
                        _ => false,
                    };
                } 
            }
            public string HairColour { get; private set; }
            public bool IsHairColourValid => Regex.IsMatch(HairColour, @"^#[0-9a-f]{6}$");

            public string EyeColour { get; private set; }
            public bool IsEyeColourValid => new[] { "amb", "blu", "brn", "gry", "grn", "hzl", "oth" }.Contains(EyeColour);
            public string PassportId { get; private set; }
            public bool IsPassportIdValid => Regex.IsMatch(PassportId, @"^[\d]{9}$");
            public string CountryId { get; private set; }

            public bool IsDirty { get; private set; } = false;
            public bool IsComplete =>
                !string.IsNullOrEmpty(BirthYear) && !string.IsNullOrEmpty(IssueYear) && !string.IsNullOrEmpty(ExpirationYear) &&
                !string.IsNullOrEmpty(Height) && !string.IsNullOrEmpty(HairColour) &&
                !string.IsNullOrEmpty(EyeColour) && !string.IsNullOrEmpty(PassportId);

            public bool IsValid
            {
                get
                {
                    if (!IsComplete) {
                        //Console.WriteLine($"Incomplete{Environment.NewLine}");    
                        return false; 
                    }
                    var valid = IsBirthYearValid && IsIssueYearValid && IsExpirationYearValid &&
                        IsHeightValid && IsHairColourValid && IsEyeColourValid && IsPassportIdValid;

                    //if (!valid)
                    //{
                    //    if (!IsBirthYearValid) Console.WriteLine($"Birth Year {BirthYear} invalid!");
                    //    if (!IsIssueYearValid) Console.WriteLine($"Issue Year {IssueYear} invalid!");
                    //    if (!IsExpirationYearValid) Console.WriteLine($"Expiration Year {ExpirationYear} invalid!");
                    //    if (!IsHeightValid) Console.WriteLine($"Height {Height} invalid!");
                    //    if (!IsHairColourValid) Console.WriteLine($"Hair Colour {HairColour} invalid!");
                    //    if (!IsEyeColourValid) Console.WriteLine($"Eye Colour {EyeColour} invalid!");
                    //    if (!IsPassportIdValid) Console.WriteLine($"Passport Id {PassportId} invalid!");
                    //    Console.WriteLine("");
                    //}

                    return valid;
                }
            }
                

            public void LoadDetailLine(string detailLine)
            {
                var details = detailLine.Split(" ");
                foreach (var detail in details)
                {
                    var (key, value, _) = detail.Split(":");
                    switch (key)
                    {
                        case "byr": BirthYear = value; break;
                        case "iyr": IssueYear = value; break;
                        case "eyr": ExpirationYear = value; break;
                        case "hgt": Height = value; break;
                        case "hcl": HairColour = value; break;
                        case "ecl": EyeColour = value; break;
                        case "pid": PassportId = value; break;
                        case "cid": CountryId = value; break;
                    }
                }

                IsDirty = true;
            }
        }
    }

    public static class Extensions
    {
        public static void Deconstruct<T>(this IList<T> list, out T first, out IList<T> rest)
        {

            first = list.Count > 0 ? list[0] : default; // or throw
            rest = list.Skip(1).ToList();
        }

        public static void Deconstruct<T>(this IList<T> list, out T first, out T second, out IList<T> rest)
        {
            first = list.Count > 0 ? list[0] : default; // or throw
            second = list.Count > 1 ? list[1] : default; // or throw
            rest = list.Skip(2).ToList();
        }
    }
}
