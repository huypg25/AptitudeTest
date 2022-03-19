
using AptidudeTest.Data.Static;
using AptidudeTest.Models;
using Microsoft.AspNetCore.Identity;

namespace AptidudeTest.Data
{
    public class AppDbInitializer
    {
        public static async Task SeedUsersAndRolesAsync(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                //Roles
                var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                if (!await roleManager.RoleExistsAsync(Roles.Manager)) await roleManager.CreateAsync(new IdentityRole(Roles.Manager));
                if (!await roleManager.RoleExistsAsync(Roles.Candidate)) await roleManager.CreateAsync(new IdentityRole(Roles.Candidate));

                //Users
                var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
                var adminUser = await userManager.FindByEmailAsync("admin@filmhub.com");
                if (adminUser == null)
                {
                    var newAdminUser = new ApplicationUser()
                    {
                        FullName = "Super Admin",
                        UserName = "admin",
                        Email = "admin@yolo.com",
                        EmailConfirmed = true
                    };
                    await userManager.CreateAsync(newAdminUser, "pAssword123456@");
                    await userManager.AddToRoleAsync(newAdminUser, Roles.Manager);
                }
                var appUser = await userManager.FindByEmailAsync("user@filmhub.com");
                if (appUser == null)
                {
                    var newAppUser = new ApplicationUser()
                    {
                        FullName = "Candidate",
                        UserName = "candidate",
                        Email = "candidate@yolo.com",
                        EmailConfirmed = true
                    };
                    await userManager.CreateAsync(newAppUser, "pAssword123456@");
                    await userManager.AddToRoleAsync(newAppUser, Roles.Candidate);
                }
            }
        }

        public static void Seed(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {

                var context = serviceScope.ServiceProvider.GetService<ApplicationDbContext>();
                context.Database.EnsureCreated();

                //Exam
                if (!context.Exams.Any())
                {
                    context.Exams.AddRange(new List<Exam>()
                    {
                        new Exam()
                        {
                             ExamName = "General Knowledge",
                             PassScore = 50,
                             Status = 1,
                             Time = 15
                        },
                         new Exam
                         {
                             ExamName = "Mathematics",
                             PassScore = 80,
                             Status = 1,
                             Time = 20,
                         },
                          new Exam
                          {
                              ExamName = "Computer Technology",
                              PassScore = 70,
                              Status = 1,
                              Time=25
                          }
                    });
                    context.SaveChanges();
                }
                //Question
                if (!context.Questions.Any())
                {
                    context.Questions.AddRange(new List<Question>()
                    {
                        new Question()
                        {
                            Text = "Grand Central Terminal, Park Avenue, New York is the world's",
                            Point = 10,
                            ExamId = 1,
                        },
                        new Question()
                        {
                            Text = "Entomology is the science that studies",
                            Point = 10,
                            ExamId = 1,
                        },
                        new Question()
                        {
                            Text = "Eritrea, which became the 182nd member of the UN in 1993, is in the continent of",
                            Point = 10,
                            ExamId = 1,
                        },

                    });
                    context.SaveChanges();
                }
                //Choice
                if (!context.Choices.Any())
                {
                    context.Choices.AddRange(new List<Choice>()
                    {
                        //Question 1
                        new Choice()
                        {
                             Text = "largest railway station",
                             QuestionId = 1,
                             IsCorrect = true,
                        },
                           new Choice()
                        {
                             Text = "highest railway station",
                             QuestionId = 1,
                             IsCorrect = false,
                        },
                              new Choice()
                        {
                             Text = "longest railway station",
                             QuestionId = 1,
                             IsCorrect = false,
                        },
                                 new Choice()
                        {
                             Text = "None of the above",
                             QuestionId = 1,
                             IsCorrect = false,
                        },
                                 //Question 2
                        new Choice()
                        {
                             Text = "Behavior of human beings",
                             QuestionId = 2,
                             IsCorrect = false,
                        },
                           new Choice()
                        {
                             Text = "Insects",
                             QuestionId = 2,
                             IsCorrect = true,
                        },
                              new Choice()
                        {
                             Text = "The origin and history of technical and scientific terms",
                             QuestionId = 2,
                             IsCorrect = false,
                        },
                                 new Choice()
                        {
                             Text = "The formation of rocks",
                             QuestionId = 2,
                             IsCorrect = false,
                        },
                                            //Question 3
                        new Choice()
                        {
                             Text = "Asia",
                             QuestionId = 3,
                             IsCorrect = false,
                        },
                           new Choice()
                        {
                             Text = "Africa",
                             QuestionId = 3,
                             IsCorrect = true,
                        },
                              new Choice()
                        {
                             Text = "Europe",
                             QuestionId = 3,
                             IsCorrect = false,
                        },
                                 new Choice()
                        {
                             Text = "Australia",
                             QuestionId = 3,
                             IsCorrect = false,
                        },

                    });
                    context.SaveChanges();
                }
            }
        }

    }
}