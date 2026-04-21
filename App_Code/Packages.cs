using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

public class Package
{
    public int Id { get; set; }
    public string PackageType { get; set; }
    public string PackageName { get; set; }
    public string PackageUrl { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public string Status { get; set; }
}
public static class PackageService
{
    public static List<Package> GetAllPackages()
    {
        var packages = new List<Package>();

        try
        {
            var corporate1 = new Package
            {
                Id = 1,
                PackageType = "Corporate",
                PackageName = "Skill & Adventure 1",
                PackageUrl = "skill-and-adventure-1",
                Description = @"<p class='mb-1'>
                                                  <img width='24' height='24' class='me-2' src='https://img.icons8.com/material/24/94D82D/trekking.png' alt='trekking' />Pipeline (TB)
                                              </p>
                                              <p class='mb-1'>
                                                  <img width='24' height='24' class='me-2' src='https://img.icons8.com/material/24/94D82D/turtle.png' alt='turtle' />Magic Turtle (TB)
                                              </p>
                                              <p class='mb-1'>
                                                  <img idth='24' height='24' class='me-2' src='https://img.icons8.com/ios-glyphs/24/94D82D/archer.png' alt='archer' />Archery Tag
                                              </p>
                                              <p class='mb-1'>
                                                  <img width='24' height='24' class='me-2' src='https://img.icons8.com/material-sharp/24/94D82D/football.png' alt='football' />Bubble Soccer
                                              </p>
                                              <p class='mb-4'>
                                                  <img width='24' height='24' class='me-2' src='https://img.icons8.com/external-flatart-icons-outline-flatarticons/24/94D82D/external-foosball-arcade-flatart-icons-outline-flatarticons.png' alt='external-foosball-arcade-flatart-icons-outline-flatarticons' />Human Foosball
                                              </p>",
                Price = 1800,
                Status = "Active"
            };

            var corporate2 = new Package
            {
                Id = 2,
                PackageType = "Corporate",
                PackageName = "Skill & Adventure 2",
                PackageUrl = "skill-and-adventure-2",
                Description = @"<p class='mb-1'>
                                                  <img width='24' height='24' class='me-2' src='https://img.icons8.com/material/24/94D82D/trekking.png' alt='trekking' />
                                                  Pipeline (TB)
                                              </p>
                                              <p class='mb-1'>
                                                  <img width='24' height='24' class='me-2' src='https://img.icons8.com/material/24/94D82D/turtle.png' alt='turtle' />Magic Turtle (TB)
                                              </p>
                                              <p class='mb-1'>
                                                  <img width='24' height='24' class='me-2' src='https://img.icons8.com/ios/50/94D82D/pyramid-toy.png' alt='pyramid-toy' />Pyramid builder (TB)
                                              </p>
                                              <p class='mb-1'>
                                                  <img idth='24' height='24' class='me-2' src='https://img.icons8.com/ios-glyphs/24/94D82D/archer.png' alt='archer' />

                                                  Archery Tag
                                              </p>
                                              <p class='mb-4'>
                                                  <img width='24' height='24' class='me-2' src='https://img.icons8.com/external-flatart-icons-outline-flatarticons/24/94D82D/external-foosball-arcade-flatart-icons-outline-flatarticons.png' alt='external-foosball-arcade-flatart-icons-outline-flatarticons' />Human Foosball
                                              </p>",
                Price = 1800,
                Status = "Active"
            };

            var corporate3 = new Package
            {
                Id = 3,
                PackageType = "Corporate",
                PackageName = "Skill & Adventure 3",
                PackageUrl = "skill-and-adventure-3",
                Description = @"<p class='mb-1'>
                                              <img width='24' height='24' class='me-2' src='https://img.icons8.com/material/24/94D82D/trekking.png' alt='trekking' />
                                              Pipeline (TB)
                                          </p>
                                          <p class='mb-1'>
                                              <img width='24' height='24' class='me-2' src='https://img.icons8.com/material/24/94D82D/turtle.png' alt='turtle' />Magic Turtle (TB)
                                          </p>
                                          <p class='mb-1'>
                                              <img width='24' height='24' class='me-2' src='https://img.icons8.com/ios/50/94D82D/pyramid-toy.png' alt='pyramid-toy' />Pyramid builder (TB)
                                          </p>
                                          <p class='mb-1'>
                                              <img width='24' height='24' class='me-2' src='https://img.icons8.com/ios-filled/50/94D82D/forward-punch.png' alt='forward-punch' />Key Punch
                                          </p>
                                          <p class='mb-4'>
                                              <img width='24' height='24' class='me-2' src='https://img.icons8.com/external-flatart-icons-outline-flatarticons/24/94D82D/external-foosball-arcade-flatart-icons-outline-flatarticons.png' alt='external-foosball-arcade-flatart-icons-outline-flatarticons' />Human Foosball
                                          </p>",
                Price = 1800,
                Status = "Active"
            };

            var adventure1 = new Package
            {
                Id = 4,
                PackageType = "Adventure",
                PackageName = "Sportico Brave Heart",
                PackageUrl = "sportico-brave-heart",
                Description = @"<p class='mb-1'>
                                              <img width='24' height='24' class='me-2' src='https://img.icons8.com/fluency-systems-filled/48/94d82d/walking-skin-type-1.png' alt='walking-skin-type-1' />High Rope Course
                                          </p>
                                          <p class='mb-1'>
                                              <img width='24' height='24' class='me-2' src='https://img.icons8.com/ios/50/94d82d/zipline.png' alt='zipline' />Zipline
                                          </p>
                                          <p class='mb-1'>
                                              <img width='24' height='24' class='me-2' src='https://img.icons8.com/ios/50/94d82d/climbing-wall.png' alt='climbing-wall' />Climbing Wall
                                          </p>
                                          <p class='mb-1'>
                                              <img width='24' height='24' class='me-2' src='https://img.icons8.com/ios/94d82d/old-king.png' alt='old-king' />Kayaking
                                          </p>
                                          <p class='mb-4'>
                                              <img width='24' height='24' class='me-2' src='https://img.icons8.com/ios-glyphs/30/94d82d/drop-zone.png' alt='drop-zone' />Free Fall
                                          </p>",
                Price = 3350,
                Status = "Active"
            };

            var adventure2 = new Package
            {
                Id = 5,
                PackageType = "Adventure",
                PackageName = "Sportico Team Player",
                PackageUrl = "sportico-team-player",
                Description = @"<p class='mb-1'>
                                              <img idth='24' height='24' class='me-2' src='https://img.icons8.com/ios-glyphs/24/94D82D/archer.png' alt='archer' />Archery Tag
                                          </p>
                                          <p class='mb-1'>
                                              <img width='24' height='24' class='me-2' src='https://img.icons8.com/material-sharp/24/94D82D/football.png' alt='football' />Bubble Soccer
                                          </p>
                                          <p class='mb-1'>
                                              <img width='24' height='24' class='me-2' src='https://img.icons8.com/external-flatart-icons-outline-flatarticons/24/94D82D/external-foosball-arcade-flatart-icons-outline-flatarticons.png' alt='external-foosball-arcade-flatart-icons-outline-flatarticons' />Human Foosball
                                          </p>
                                          <p class='mb-1'>
                                              <img width='24' height='24' class='me-2' src='https://img.icons8.com/ios/94d82d/old-king.png' alt='old-king' />Kayaking
                                          </p>
                                          <p class='mb-4'>
                                              <img width='24' height='24' class='me-2' src='https://img.icons8.com/ios/50/94d82d/fondue.png' alt='fondue' />MeltDown
                                          </p>",
                Price = 1800,
                Status = "Active"
            };

            var adventure3 = new Package
            {
                Id = 6,
                PackageType = "Adventure",
                PackageName = "Sportico Warrior",
                PackageUrl = "sportico-warrior",
                Description = @"<p class='mb-1'>
                                              <img width='24' height='24' class='me-2' src='https://img.icons8.com/ios/50/94d82d/tennis-ball.png' alt='tennis-ball' />Paintball
                                          </p>
                                          <p class='mb-1'>
                                              <img idth='24' height='24' class='me-2' src='https://img.icons8.com/ios-glyphs/24/94D82D/archer.png' alt='archer' />

                                              Archery Tag
                                          </p>
                                          <p class='mb-1'>
                                              <img width='24' height='24' class='me-2' src='https://img.icons8.com/material-sharp/24/94D82D/football.png' alt='football' />Bubble Soccer
                                          </p>
                                          <p class='mb-4'>
                                              <img width='24' height='24' class='me-2' src='https://img.icons8.com/external-flatart-icons-outline-flatarticons/24/94D82D/external-foosball-arcade-flatart-icons-outline-flatarticons.png' alt='external-foosball-arcade-flatart-icons-outline-flatarticons' />Human Foosball
                                          </p>",
                Price = 2100,
                Status = "Active"
            };

            var adventure4 = new Package
            {
                Id = 7,
                PackageType = "Adventure",
                PackageName = "Sportico Racer",
                PackageUrl = "sportico-racer",
                Description = @"<p class='mb-1'>
                                              <img width='24' height='24' class='me-2' src='https://img.icons8.com/ios/50/94d82d/tennis-ball.png' alt='tennis-ball' />Paintball
                                          </p>
                                          <p class='mb-1'>
                                              <img idth='24' height='24' class='me-2' src='https://img.icons8.com/ios-glyphs/24/94D82D/archer.png' alt='archer' />Archery Tag
                                          </p>
                                          <p class='mb-1'>
                                              <img width='24' height='24' class='me-2' src='https://img.icons8.com/material-sharp/24/94D82D/football.png' alt='football' />Bubble Soccer
                                          </p>
                                          <p class='mb-4'>
                                              <img width='24' height='24' class='me-2' src='https://img.icons8.com/external-flatart-icons-outline-flatarticons/24/94D82D/external-foosball-arcade-flatart-icons-outline-flatarticons.png' alt='external-foosball-arcade-flatart-icons-outline-flatarticons' />Human Foosball
                                          </p>",
                Price = 3100,
                Status = "Active"
            };

            var adventure5 = new Package
            {
                Id = 8,
                PackageType = "Adventure",
                PackageName = "Sportico Fly High",
                PackageUrl = "sportico-fly-high",
                Description = @"<p class='mb-1'>
                                              <img width='24' height='24' class='me-2' src='https://img.icons8.com/fluency-systems-filled/48/94d82d/walking-skin-type-1.png' alt='walking-skin-type-1' />High Rope Course
                                          </p>
                                          <p class='mb-1'>
                                              <img width='24' height='24' class='me-2' src='https://img.icons8.com/ios/50/94d82d/zipline.png' alt='zipline' />Zipline
                                          </p>
                                          <p class='mb-1'>
                                              <img width='24' height='24' class='me-2' src='https://img.icons8.com/ios/50/94d82d/climbing-wall.png' alt='climbing-wall' />Climbing Wall
                                          </p>
                                          <p class='mb-4'>
                                              <img width='24' height='24' class='me-2' src='https://img.icons8.com/ios-glyphs/30/94d82d/drop-zone.png' alt='drop-zone' />Free Fall
                                          </p>",
                Price = 3250,
                Status = "Active"
            };


            var individual1 = new Package
            {
                Id = 9,
                PackageType = "Individual",
                PackageName = "Free Fall",
                PackageUrl = "free-fall",
                Description = @"<p class='mb-4'><img width='24' height='24' class='me-2' src='https://img.icons8.com/ios-glyphs/30/94d82d/drop-zone.png' alt='drop-zone' />Free Fall</p>",
                Price = 800,
                Status = "Active"
            };

            packages.Add(corporate1);
            packages.Add(corporate2);
            packages.Add(corporate3);
            packages.Add(adventure1);
            packages.Add(adventure2);
            packages.Add(adventure3);
            packages.Add(adventure4);
            packages.Add(adventure5);
            packages.Add(individual1);
        }
        catch (Exception ex)
        {
            return null;
        }

        return packages;
    }
}
