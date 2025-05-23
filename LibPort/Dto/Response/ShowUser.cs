﻿using LibPort.Models;

namespace LibPort.Dto.Response
{
    public class ShowUser
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public UserType UserType { get; set; }
    }
}
