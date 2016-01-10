using CourseServer.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseServer.Entities
{
    public class UserEntity : Model.Model
    {
        /// <summary>
        /// That's ME :)
        /// </summary>
        private const string DEFAULT_AVATAR = "http://i.imgur.com/OBWaenZ.jpg";

        public UserEntity()
        {
            Avatar = DEFAULT_AVATAR;
        }

        private string avatar;
        public string Avatar
        {
            get
            {
                return avatar;
            }
            set
            {
                if (! TextUtils.isEmpty(value))
                {
                    avatar = value;
                }
            }
        }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        public string Cellphone { get; set; }

        [NotMapped]
        public int Mode { get; set; }
    }
}
