using Data.Entities;

namespace Data
{
    public class EntitiesRepository
    {
        private readonly List<PostDto> _postRepository = new();
        private readonly List<UserDto> _userRepository = new();
        private readonly List<CommentDto> _commentRepository = new();

        public EntitiesRepository()
        {
            if (_postRepository == null)
                _postRepository = new List<PostDto>();
            if (_userRepository == null)
                _userRepository = new List<UserDto>();
            if (_commentRepository == null)
                _commentRepository = new List<CommentDto>();
        }

        #region POSTS MANAGEMENT
        private void AttachCommentsToPost(PostDto post)
        {
            var comments = _commentRepository.FindAll(c => c.PostId == post.Id);
            if (comments != null && comments.Any())
            {
                post.Comments = new List<CommentDto>();
                post.Comments.AddRange(comments);
            }
        }

        public List<PostDto> GetAllPostsByUserName(string userName)
        {
            var posts = _postRepository.FindAll(p => p.Author.Equals(userName));
            posts.ForEach(p => {
                AttachCommentsToPost(p);
            });
            
            return posts;
        }

        public PostDto? GetPostById(int id)
        {
            var post = _postRepository.FirstOrDefault(p => p.Id == id);
            if (post == null) return new PostDto();
            AttachCommentsToPost(post);
                
            return post;
        }

        public int AddNewPost(string author, string title, string text)
        {
            var newPost = new PostDto
            {
                Author = author,
                Id = _postRepository.Count + 1,
                Title = title,
                Text = text
            };

            _postRepository.Add(newPost);
            return newPost.Id;
        }

        public int? EditPost(int id, string newTitle, string newText)
        {
            var oldPost = _postRepository.FirstOrDefault(p => p.Id == id);
            if (oldPost == null) return null;

            oldPost.Title = newTitle ?? oldPost.Title;
            oldPost.Text = newText ?? oldPost.Text;
            return id;
        }

        public int? DeletePost(int id)
        {
            var post = _postRepository.Where(p => p.Id == id).FirstOrDefault();
            if (post == null) return null;

            var removed = _postRepository.Remove(post);
            return removed ? id : null;
        }
        #endregion

        #region USERS MANAGEMENT
        public UserDto GetUserById(int id)
        {
            return _userRepository.First(u => u.UserId.Equals(id));
        }

        public UserDto? GetUserByUserName(string userName)
        {
            return _userRepository.FirstOrDefault(u => u.UserName.Equals(userName.Trim()));
        }

        public int? InsertNewUser(string userName)
        {
            var existingUser = _userRepository.FirstOrDefault(u => u.UserName.Equals(userName.Trim()));
            if (existingUser != null) return null;

            var newUser = new UserDto
            {
                UserId = _userRepository.Count + 1,
                UserName = userName.Trim()
            };

            _userRepository.Add(newUser);
            return newUser.UserId;
        }

        public int? DeleteUser(int id)
        {
            var user = _userRepository.Where(u => u.UserId == id).FirstOrDefault();
            if (user == null) return null;

            var removed = _userRepository.Remove(user);
            return removed ? id : null;
        }
        #endregion

        #region COMMENTS MANAGEMENT
        public List<CommentDto> GetCommentsByPostId(int postId)
        {
            return _commentRepository.Where(u => u.PostId == postId).ToList();
        }

        public CommentDto? GetCommentById(int id)
        {
            return _commentRepository.Where(u => u.Id == id).FirstOrDefault();
        }

        public int AddNewComment(int postId, int authorId, string text)
        {
            var newComment = new CommentDto
            {
                AuthorId = authorId,
                Id = _commentRepository.Count + 1,
                PostId = postId,
                Text = text
            };

            _commentRepository.Add(newComment);
            return newComment.Id;
        }

        public int? EditComment(int id, string newText)
        {
            var oldComment = _commentRepository.Where(u => u.Id == id).FirstOrDefault();
            if (oldComment == null) return null;

            oldComment.Text = newText;
            return oldComment.Id;
        }

        public int? DeleteComment(int id)
        {
            var oldComment = _commentRepository.Where(u => u.Id == id).FirstOrDefault();
            if (oldComment == null) return null;

            _commentRepository.Remove(oldComment);
            return oldComment.Id;
        }
        #endregion
    }
}
