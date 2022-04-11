using Data;
using Data.Entities;

namespace Business
{
    public class EntitiesManager
    {
        private readonly EntitiesRepository _entitiesRepository;

        public EntitiesManager(EntitiesRepository entitiesRepository)
        {
            _entitiesRepository = entitiesRepository;
        }

        #region POSTS MANAGEMENT
        public int AddNewPost(string userName, string title, string text)
        {
            return _entitiesRepository.AddNewPost(userName, title, text);
        }

        public int EditPost(int id, string newTitle, string newText)
        {
            var result = _entitiesRepository.EditPost(id, newTitle, newText);
            if (!result.HasValue) throw new Exception("Errore nella modifica");
            return result.Value;
        }

        public List<PostDto> GetAllPostsByUserName(string userName)
        {
            if (string.IsNullOrEmpty(userName)) return new List<PostDto>();
            return _entitiesRepository.GetAllPostsByUserName(userName);
        }

        public PostDto? GetPostById(int id)
        {
            return _entitiesRepository.GetPostById(id);
        }

        public int DeletePost(int id)
        {
            var result = _entitiesRepository.DeletePost(id);
            if (!result.HasValue) throw new Exception("Errore nella cancellazione");
            return result.Value;
        }
        #endregion

        #region USERS MANAGEMENT
        public int? AddNewUser(string userName)
        {
            return _entitiesRepository.InsertNewUser(userName) ?? throw new Exception("Nome utente non disponibile");
        }

        public UserDto GetUserById(int id)
        {
            return _entitiesRepository.GetUserById(id);
        }

        public UserDto? GetUserByUserName(string userName)
        {
            return _entitiesRepository.GetUserByUserName(userName);
        }

        public int DeleteUser(int id)
        {
            var deleted = _entitiesRepository.DeleteUser(id);
            if (deleted == null) throw new Exception("Errore cancellazione utente");

            return deleted.Value;
        }
        #endregion

        #region COMMENTS MANAGEMENT
        public int AddNewComment(int postId, int authorId, string text)
        {
            var user = _entitiesRepository.GetUserById(authorId);
            if (user == null) throw new Exception("Utente non esistente");

            var post = _entitiesRepository.GetPostById(postId);
            if (post == null) throw new Exception("Post non esistente");

            return _entitiesRepository.AddNewComment(postId, authorId, text);
        }

        public CommentDto? GetCommentById(int id)
        {
            return _entitiesRepository.GetCommentById(id);
        }

        public int? EditComment(int id, string newText)
        {
            var edit = _entitiesRepository.EditComment(id, newText);
            if (edit == null) throw new Exception("Errore modifica del commento");

            return edit.Value;
        }

        public int? DeleteComment(int id)
        {
            var deleted = _entitiesRepository.DeleteComment(id);
            if (deleted == null) throw new Exception("Errore cancellazione del commento");

            return deleted.Value;
        }
        #endregion
    }
}