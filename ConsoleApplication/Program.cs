using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Class.Domain;
using ClassDomainModel;

namespace ConsoleApplication
{
    class Program
    {
        static void Main(string[] args)
        {//Com esse script não irá verificar as minhas classes do programas(model), só o banco
            Database.SetInitializer(new NullDatabaseInitializer<ClassContext>());
            //Aprendendo a adicionar -------------------
            //AddAuthor("Ribeiro", 59);
            //addStudents();
            //addCourse("C# para mestres", 1);
            //addMultCourse();
            //addDataRelationally();

            //Aprendendo a fazer uma busca ----------------
            //searchCourse();
            //searchCourseByIdAuthorUsingJoin(1);
            //searchCourseFirstList(1);
            //searchUsingFindCourse();
            //searchWithStoredProceduresByAuthors();
            //searchAuthorWithCourseByEagerExplicitLazy();
            //searchProjection();

            //Aprendendo a fazer atualizações no banco ------------------
            //updateAppConnected();
            // updateAppDisconected();

            //Aprendendo o Delete ------------------
            //deleteCourseSimpleForm();
            //deleteCourseAppRealDisconected();
            //deleteWithStoredProcedure();
        }

        //--------------------------------remove--------------------------------
        private static void deleteWithStoredProcedure()
        {
            var keyVal = 4;
            using (var context = new ClassContext())
            {
                context.Database.Log = Console.WriteLine;
                //dessa forma estará executando um comando sql e executei uma stored Procedures
                context.Database.ExecuteSqlCommand(
                    "exec DeleteCourseById {0}", keyVal);//("...", parametro)
            }
        }

        private static void deleteCourseAppRealDisconected()
        {
            Course course;
            //primeiro a busca do que desejo remover
            using (var context = new ClassContext())
            {
                context.Database.Log = Console.WriteLine;
                course = context.Course.FirstOrDefault();
            }
            //aqui faz a remoção 
            using (var context = new ClassContext())
            {
                context.Database.Log = Console.WriteLine;
                context.Entry(course).State = EntityState.Deleted;
                context.SaveChanges();
            }
        }

        private static void deleteCourseSimpleForm()
        {
            using (var context = new ClassContext())
            {
                context.Database.Log = Console.WriteLine;
                //faço a busca do objeto que desejo
                var query = context.Course.FirstOrDefault();
                //informa que desejo deletar
                context.Course.Remove(query);
                //salvo o meu remove
                context.SaveChanges();

            }
        }



        //-------------------------upDate---------------------------------
        //será em feito em sites e web API (aplicações desconectadas)
        private static void updateAppDisconected()
        {
            Course course;
            //aqui é feito a conexão quando entra no app para o usuairo conseguir mexer
            //e selecionado o que irá ser mandado para o usuario
            using (var context = new ClassContext())
            {
                context.Database.Log = Console.WriteLine;
                course = context.Course.FirstOrDefault();
            }
            //depois fecha a conexao e o usuario na pagina pode editar algo 
            // durante sua conexao
            course.AuthorId = 1;

            //depois é preciso salvar todas as alterações que foram feitas
            using (var context = new ClassContext())
            {
                context.Database.Log = Console.WriteLine;
                //context.Course.Attach(course); - campo não obrigatório, porém serve para avisar para ter atenção nesse objeto

                //para fazer o update é preciso pegar o objeto e dizer qual o estado dele para poder salvar
                context.Entry(course).State = EntityState.Modified;
                context.SaveChanges();//salva as alterações
            }
        }

        private static void updateAppConnected()
        {
            using (var context = new ClassContext())
            {
                context.Database.Log = Console.WriteLine;
                //aqui especifico onde desejo modificar
                var course = context.Course.FirstOrDefault();
                //o campo que irei modificar de acordo com o id encontrado 
                course.AuthorId = 1;
                context.SaveChanges();
                //importante no final sempre salvar a alteração que desejo fazer
            }
        }



        //-------------------------Busca-------------------------------------------

        private static void searchProjection()
        {
            using (var context = new ClassContext())
            {
                var query = context.Author.Select(c => new
                {
                    c.Name,
                    c.Age,
                    c.Course
                }).ToList();

                foreach (var item in query)
                {
                    Console.WriteLine(item);

                }
                foreach (var item in query)
                {
                    foreach (var item2 in item.Course)
                    {
                        Console.WriteLine(item2.Name);
                    }
                }
            }
        }

        private static void searchAuthorWithCourseByEagerExplicitLazy()
        {
            using (var context = new ClassContext())
            {
                //usando Eager loading - quando já sei o que devo carregar
                var query = context.Author.Include(a => a.Course)
                                .FirstOrDefault(n => n.Name.StartsWith("Raphael"));
                //Console.WriteLine(query.Name + " ---  "+ query.Course.Count);
                //comando carrega todo os dados
                //(explicit lazy carrega as informações dos cursos -context.Entry(query).Collection(c => c.Course).Load();

                //dessa forma foi usado o lazy Loading(não é o aconselhado
                //pois o desempenho da aplicação e prejudicado
                //var query = context.Author.FirstOrDefault(n => n.Name.StartsWith("Raphael"));

                Console.WriteLine($"{query.Name} ---- {query.Course.Count}");

                foreach (var item in query.Course)
                {
                    Console.WriteLine(item.Name);
                }
            }
        }

        private static void searchWithStoredProceduresByAuthors()
        {//forma de usar uma busca, utilizando uma stored Procedures
            using (var context = new ClassContext())
            {
                context.Database.Log = Console.WriteLine;

                //dessa forma já deixo pre programado que tipo de consulta que desejo fazer
                //sempre botar o exec, para fazer a consulta
                var query = context.Author.SqlQuery("exec GetAgeAuthorsOld");

                foreach (var item in query)
                {
                    Console.WriteLine(item.Name);
                }
            }
        }

        //A busca usando o find ajuda na eficiencia do código, pois depois que é achado
        //na primeira busca que é feito é salvo na memória, aí quando é feito novamente a
        //segundo busca é procurado primeiramente na memória e se não encontrado é feita a
        //busca no banco e isso deixamais rápido a aplicação...
        private static void searchUsingFindCourse()
        {
            var keyVal = 2;
            using (var context = new ClassContext())
            {
                context.Database.Log = Console.WriteLine;
                //Faz a busca ultilizando o find que é pela a chave, 1º na memoria, 2º Faz no DB
                var query = context.Course.Find(keyVal);
                Console.WriteLine(query.Name);

                //Como essa chave já foi buscada ele já encontra na memória e pega as informações dela
                var query2 = context.Course.Find(keyVal);
                Console.WriteLine(query2.Name + "\n\n");

                //aqui mesmo já tenha encontrado na memória essa chave, é feito uma busca no DB
                var query3 = context.Course.Where(c => c.id == keyVal).FirstOrDefault();
                Console.WriteLine(query3.Name);
            }
        }


        private static void searchCourseFirstList(int idAuthor)
        {
            using (var context = new ClassContext())
            {
                var query = context.Course.Where(c => c.AuthorId == idAuthor)
                                            .OrderBy(n => n.Name)//ordenação
                                                .Skip(2)//para qual posição desejo pegar
                                                    .Take(1)
                                                        .FirstOrDefault();
                //.ToList();

                Console.WriteLine(query.Name);

                //foreach (var item in query)
                //{
                //        Console.WriteLine(item.Name);
                //}
            }
        }

        private static void searchCourseByIdAuthorUsingJoin(int idAuthor)
        {
            using (var context = new ClassContext())
            {
                context.Database.Log = Console.WriteLine;
                var query = context.Course.Where(c => c.AuthorId == idAuthor)
                                            .Join(context.Author,
                                                c => c.AuthorId,
                                                    a => a.id,
                                                        (c, a) => new
                                                        {
                                                            Course = c.Name,
                                                            Author = a.Name
                                                        });

                //é bom que sempre seja feito em cima da variável não da pesquisa do banco
                //assim deixa desempenho muito melhor
                foreach (var item in query)
                {
                    Console.WriteLine($"O {item.Author} é o professor do curso {item.Course}");
                }
            }
        }

        private static void searchCourse()
        {//primeira coisa na busca sempre logo abrir conexao
            using (var context = new ClassContext())
            {//retornar a tabela curso forma de lista
                var query = context.Course.ToList();


                foreach (var item in query)
                {
                    Console.WriteLine(item.id);
                    Console.WriteLine(item.Name);
                    Console.WriteLine(item.Author);
                    Console.WriteLine(item.AuthorId);
                    Console.WriteLine(item.Evaluation);
                    Console.WriteLine("\n");
                }
            }
        }

        //------------------------Adicionar --------------------------------

        private static void addDataRelationally()
        {
            //nesse caso desejei criar o Author e logo junto os cursos
            var author = new Author
            {
                Name = "Raphael",
                Age = 27
            };
            var course1 = new Course
            {
                Name = "Direito"
            };
            var course2 = new Course
            {
                Name= "Direito Civil"
            };
            using (var context = new ClassContext())
            {
                context.Database.Log = Console.WriteLine;

                //primeiramente adicion o author
                context.Author.Add(author);
                //como já instanciei de lista de course nos autores no construtor
                //é preciso só adicionar os cursos na lista que eu estou desejando
                //o entityFrameWork já ira entender que esses novos cursos criados
                //no banco irá se relacionar com o autor
                author.Course.Add(course1);
                author.Course.Add(course2);

                context.SaveChanges();
            }
        
        }
        private static void addMultCourse()
        {
            Course course = new Course()
            {
                Name = "Java",
                AuthorId = 2
            };
            Course course1 = new Course()
            {
                Name = "C++",
                AuthorId = 3
            };
            using (var context = new ClassContext())
            {
                context.Database.Log = Console.WriteLine;
                //para informar que terá mais de 1 inserção terá que usar metodo addRange, nisso terá só uma conexão com o banco
                context.Course.AddRange(new List<Course> { course, course1 });
                context.SaveChanges();
            }


        }

        private static void addCourse(string name, int authorId)
        {
            Course course = new Course
            {
                Name = name,
                AuthorId = authorId
            };

            using (var context = new ClassContext())
            {
                context.Database.Log = Console.WriteLine;
                context.Course.Add(course);
                context.SaveChanges();
            }
        }

        private static void AddAuthor(string name, int age)
        {
            Author author = new Author
            {
                Name = name,
                Age = age
            };

            using (var context = new ClassContext())
            {
                context.Database.Log = Console.WriteLine;
                context.Author.Add(author);//aqui informa que vou add algo no db
                context.SaveChanges();//aqui é feito a adição 
            }
        }
        private static void addStudents()
        {
            Student student = new Student
            {
                Name = "João"
            };
            using (var context = new ClassContext())
            {
                context.Student.Add(student);
                context.SaveChanges();
            }
        }

    }
}
