﻿using AdminGestaoApi.Domain.Enum;

namespace AdminGestaoApi.ApplicationService.Responses.Usuario
{
    public class UsuarioResponse
    {
        public long Id { get; set; }

        public string? Nome { get; set; }

        public string? Username { get; set; }

        public string? Email { get; set; }

        public RoleEnum Role { get; set; }
    }
}
