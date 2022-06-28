using Service.Models;
using DAL;

namespace Service.Services
{
    public class AuthorService
    {
        private static AuthorService? instance = null;

        public static AuthorService Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new AuthorService();
                }
                return instance;
            }
        }

        public IEnumerable<AuthorDTO> GetAuthors()
        {
            using (var context = new ExamContext())
            {
                var authors = context.Authors
                    .Select(AuthorToDTO)
                    .OrderBy(x => x.FirstName).ThenBy(x => x.LastName)
                    .ToList();

                return authors;
            }
        }

        public AuthorDTO GetAuthor(Guid id)
        {
            using (var context = new ExamContext())
            {
                var author = context.Authors.FirstOrDefault(x => x.Id == id);
                if (author == null)
                    throw new NullReferenceException("No Author found");

                var dto = AuthorToDTO(author);
                return dto;
            }
        }

        public void DeleteAuthor(Guid id)
        {
            using (var context = new ExamContext())
            {
                var author = context.Authors.FirstOrDefault(x => x.Id == id);
                if (author == null)
                    throw new NullReferenceException($"No Author found with Id: {id}");

                context.Remove(author);
                context.SaveChanges();
            }
        }

        public void CreateAuthor(CreateAuthorDTO authorDTO)
        {
            var imageFileNames = ImageService.Instance.GetAll();
            if (!imageFileNames.Contains(authorDTO.ImageName))
            {
                throw new ArgumentException("Provided imageName does not exist on server");
            }

            using (var context = new ExamContext())
            {
                var author = new Author
                {
                    Id = Guid.NewGuid(),
                    FirstName = authorDTO.FirstName,
                    LastName = authorDTO.LastName,
                    ImageName = authorDTO.ImageName,
                    Mail = authorDTO.Mail,
                    TwitterUserName = authorDTO.TwitterUserName,
                };

                context.Add(author);
                context.SaveChanges();
            }
        }

        public void UpdateAuthor(UpdateAuthorDTO authorDTO)
        {
            var imageFileNames = ImageService.Instance.GetAll();
            if (!imageFileNames.Contains(authorDTO.ImageName))
            {
                throw new ArgumentException("Provided imageName does not exist on server");
            }

            using (var context = new ExamContext())
            {
                var author = new Author
                {
                    Id = authorDTO.Id,
                    FirstName = authorDTO.FirstName,
                    LastName = authorDTO.LastName,
                    ImageName = authorDTO.ImageName,
                    Mail = authorDTO.Mail,
                    TwitterUserName = authorDTO.TwitterUserName,
                };

                context.Update(author);
                context.SaveChanges();
            }
        }

        private AuthorDTO AuthorToDTO(Author author)
        {
            return new AuthorDTO
            {
                Id = author.Id,
                FirstName = author.FirstName,
                LastName = author.LastName,
                ImageName = author.ImageName,
                TwitterUserName = author.TwitterUserName,
                Mail = author.Mail
            };
        }
    }
}
