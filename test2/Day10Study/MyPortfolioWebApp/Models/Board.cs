
using System;
using System.ComponentModel.DataAnnotations;

namespace SampleBoardProject.Models
{
    public class Board
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "제목을 입력하세요.")]
        public string Title { get; set; }

        [Required(ErrorMessage = "내용을 입력하세요.")]
        public string Content { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public string UserId { get; set; }
    }
}
