using Dapper;
using Npgsql;
using OnlineQuiz.Domain.Enums;
using OnlineQuiz.Domain.Models;
using OnlineQuiz.Infrastucture.Persistence;

namespace OnlineQuiz.Presentation
{
    public class Start:DbContext
    {
        public static async void Enter()
        {
            int index = 0;
            Category[] categories = Enum.GetValues<Category>();
            foreach (Category category in categories)
            {
                Console.WriteLine($"{++index}.{category}");
            }
            Console.Write("\nSelect: ");
            index = int.Parse(Console.ReadLine());
            using NpgsqlConnection connection = new NpgsqlConnection(_connectionString);
            await connection.OpenAsync();
            string query = $"select * from inner_categories where category_name = {categories[index - 1]}";
            var innerCategories = connection.Query<InnerCategory>(query).ToArray();
            index = 0;
            foreach (var item in innerCategories)
            {
                Console.WriteLine($"{++index}.{item.InnerCategoryName}");
            }
            Console.Write("\nSelect: ");
            index = int.Parse(Console.ReadLine());
            DbQustion dbQustion = new DbQustion();
            var dbQuestions = await dbQustion.GetAllAsync();
            Question[] questions = dbQuestions.Where(question => question.InnerCategory.InnerCategoryId == innerCategories[index-1].InnerCategoryId).ToArray();
            int[] collection = Enumerable.Range(1,questions.Length).ToArray();
            Random random = new Random();
            int[] numbers = collection.OrderBy(x=>random.Next()).Distinct().Take(20).ToArray(); 
            List<int> truAanswers = new List<int>();
            foreach(int i in numbers)
            {
                index = 0;
                Console.WriteLine($@"{questions[i].QuestionText}
                                    \n{++index}.{questions[i].Option1}
                                    \n{++index}.{questions[i].Option2}
                                    \n{++index}.{questions[i].Option3}
                                    \n{++index}.{questions[i].Option4}");
                Console.Write("\nSelect: ");
                index = int.Parse(Console.ReadLine());
                if (index == questions[i].TrueOption)
                {
                    truAanswers.Add(questions[i].QuestionId);
                }
            }
        }
        public async static void SignIn()
        {
            DbAccount dbAccount = new DbAccount();
            Account account = new Account();
            Console.Write("Username : ");
            account.Username = Console.ReadLine() ?? "";
            Console.Write("Password : ");
            account.Password = Console.ReadLine() ?? "";
            List<Account> accounts = dbAccount.GetAllAsync().Result.ToList();
            if ((accounts.SingleOrDefault(x => x.Username == account.Username && x.Password == account.Password)) is not null)
            {
                Console.WriteLine("Sign In successfully");
                Console.WriteLine("1.Get InnerCategorys");
                Console.WriteLine("0.Create InnerCategory");
                byte n = byte.Parse(Console.ReadLine());
                InnerCategory category = new InnerCategory();
                if (n == 1)
                {
                    category = GetInnerCategorys();
                    CreateQuestion(category);
                }
                else
                {
                    category = CreateInnerCategory();
                    CreateQuestion(category);
                }

            }
        }
        public async static void SignUp()
        {
            Account account = new Account();
            Console.Write("Enter a username : ");
            account.Username = Console.ReadLine() ?? "";
            Console.WriteLine("Example : +998XXXXXXXXX");
            Console.Write("Enter the user phone number : ");
            account.PhoneNumber = Console.ReadLine() ?? "";
            Console.Write("Email : ");
            account.Email = Console.ReadLine() ?? "";
            Console.Write("Password : ");
            account.Password = Console.ReadLine() ?? "";
            DbAccount dbAccount = new DbAccount();
            await dbAccount.AddAsync(account);
            Console.WriteLine("Sign Up successfully");
        }




        public static InnerCategory CreateInnerCategory()
        {
            InnerCategory category = new InnerCategory();
            Console.Write("InnerCategoryName : ");
            category.InnerCategoryName = Console.ReadLine() ?? "";            
            Console.Write("Time in minutes : ");
            category.Time = TimeSpan.FromMinutes(int.Parse(Console.ReadLine()));
            byte i = 1;
            foreach (var item in Enum.GetValues(typeof(Category)))
            {
                Console.WriteLine($"{i}. " + item);
                i++;
            }
            Console.Write("Categoty : ");
            byte s = byte.Parse(Console.ReadLine() ?? "1");
            category.CategoryName = ((Category)(s - 1));
            DbInnerCategory dbInnerCategory = new DbInnerCategory();
            dbInnerCategory.AddAsync(category).Wait();
            return category;
        }
        public async static void CreateQuestion(InnerCategory category)
        {
            Question question = new Question();
            Console.Write("QuestionText : ");
            question.QuestionText = Console.ReadLine() ?? "";
            Console.Write("Option1 : ");
            question.Option1 = Console.ReadLine() ?? "";
            Console.Write("Option2 : ");
            question.Option2 = Console.ReadLine() ?? "";
            Console.Write("Option3 : ");
            question.Option3 = Console.ReadLine() ?? "";
            Console.Write("Option4 : ");
            question.Option4 = Console.ReadLine() ?? "";
            Console.Write("TrueOption : ");
            question.TrueOption = byte.Parse(Console.ReadLine() ?? "1");
            byte i = 1;
            foreach (var item in Enum.GetValues(typeof(Difficulty)))
            {
                Console.WriteLine($"{i}. " + item);
                i++;
            }
            Console.Write("Difficulty : ");
            byte s = byte.Parse(Console.ReadLine() ?? "1");
            question.Difficulty = ((Difficulty)(s - 1));
            question.InnerCategory = category;
            DbQustion dbQustion = new DbQustion();
            await dbQustion.AddAsync(question);
        }
        public static InnerCategory GetInnerCategorys()
        {
            DbInnerCategory dbInnerCategory = new DbInnerCategory();
            InnerCategory innerCategory = new InnerCategory();
            byte n = 1;
            foreach (InnerCategory category in dbInnerCategory.GetAllAsync().Result)
            {
                Console.WriteLine($"{n}. {category.InnerCategoryName}");
                n++;
            }
            byte categorynum = byte.Parse(Console.ReadLine() ?? "1");
            List<InnerCategory> list = dbInnerCategory.GetAllAsync().Result.ToList();
            return list[categorynum - 1];
        }
    }
}
    