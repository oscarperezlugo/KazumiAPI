﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data.Entity;
using KazumiAPI.Models;
using System.Configuration;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Web.Http.Description;

namespace KazumiAPI.Controllers
{
    public class LoginController : ApiController
    {
        private kazumiEntities db = new kazumiEntities();

        //string TokenRes;
        //int ID;
        //public IHttpActionResult VerifyPassword(Models.Request.Login user)
        //{
        //    {
        //        var myUser = db.usuario.FirstOrDefault(u => u.nombre_usuario == user.User && u.password == user.Pass);
        //        if (myUser != null)
        //        {
        //            string token = "11a1ac2d7df61d02578f299669e09815";
        //            ID = myUser.id_usuario;
        //            TokenRes = token;

        //        }
        //        else
        //        {
        //            string token = "0";
        //            TokenRes = token;
        //            ID = 0;
        //        }
        //        return Json(new { Token = TokenRes.ToString(), id = ID });
        //    }
        //}

        // POST: api/Login
        [ResponseType(typeof(Models.Request.Login))]
        public async Task<IHttpActionResult> PostLogin(Models.Request.Login usuarioLogin)
        {
            if (usuarioLogin == null)
                return BadRequest("Usuario y Contraseña requeridos.");

            var _userInfo = await AutenticarUsuarioAsync(usuarioLogin.User, usuarioLogin.Pass);
            if (_userInfo != null)
            {
                return Ok(new { token = GenerarTokenJWT(_userInfo) });
            }
            else
            {
                return Unauthorized();
            }
        }

        // COMPROBAMOS SI EL USUARIO EXISTE EN LA BASE DE DATOS 
        private async Task<UsuarioInfo> AutenticarUsuarioAsync(string usuario, string password)
        {

            var find = db.usuario.FirstOrDefault(u => u.nombre_usuario == usuario && u.password == password);

            if (find != null)
            {
                return new UsuarioInfo()
                {
                    // Id del Usuario en el Sistema de Información (BD)
                    //Id = new Guid("B5D233F0-6EC2-4950-8CD7-F44D16EC878F"),
                    Uid = Guid.NewGuid(),
                    Id = find.id_usuario,
                    Nombre = find.nombre_usuario,
                };

            }

            return null;
        }

        // GENERAMOS EL TOKEN CON LA INFORMACIÓN DEL USUARIO
        private string GenerarTokenJWT(UsuarioInfo usuarioInfo)
        {
            // RECUPERAMOS LAS VARIABLES DE CONFIGURACIÓN
            var _ClaveSecreta = ConfigurationManager.AppSettings["ClaveSecreta"];
            var _Issuer = ConfigurationManager.AppSettings["Issuer"];
            var _Audience = ConfigurationManager.AppSettings["Audience"];
            if (!Int32.TryParse(ConfigurationManager.AppSettings["Expires"], out int _Expires))
                _Expires = 24;


            // CREAMOS EL HEADER //
            var _symmetricSecurityKey = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(_ClaveSecreta));
            var _signingCredentials = new SigningCredentials(
                    _symmetricSecurityKey, SecurityAlgorithms.HmacSha256
                );
            var _Header = new JwtHeader(_signingCredentials);

            // CREAMOS LOS CLAIMS //
            var _Claims = new[] {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.NameId, usuarioInfo.Id.ToString()),
                new Claim("nombre", usuarioInfo.Nombre),
                //new Claim("apellidos", usuarioInfo.Apellidos),
                //new Claim(JwtRegisteredClaimNames.Email, usuarioInfo.Email),
                //new Claim(ClaimTypes.Role, usuarioInfo.Rol)
            };

            // CREAMOS EL PAYLOAD //
            var _Payload = new JwtPayload(
                    issuer: _Issuer,
                    audience: _Audience,
                    claims: _Claims,
                    notBefore: DateTime.UtcNow,
                    // Exipra a la 24 horas.
                    expires: DateTime.UtcNow.AddHours(_Expires)
                );

            // GENERAMOS EL TOKEN //
            var _Token = new JwtSecurityToken(
                    _Header,
                    _Payload
                );

            return new JwtSecurityTokenHandler().WriteToken(_Token);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
