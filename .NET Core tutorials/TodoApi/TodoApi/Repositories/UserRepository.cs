using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Extensions.Configuration;
using Oracle.ManagedDataAccess.Client;
using TodoApi.Models;

namespace TodoApi.Repositories {
    public class UserRepository : IUsersRepository {
        IConfiguration _configuration;
        public UserRepository (IConfiguration configuration) {
            _configuration = configuration;
        }
        public IDbConnection Connection {
            get {
                return new OracleConnection (_configuration.GetConnectionString ("WF2DB12"));
            }
        }
        public async Task<User> GetUserDetails (int usrId) {
            using (var conn = Connection) {
                conn.Open ();
                var query = @"select
                usr.uzt_id as Id,
                usr.UZT_IMIE as FirstName,
                usr.UZT_NAZWISKO as LastName,
                usr.UZT_STANOWISKO as Position,
                usr.UZT_LOGIN as Login,
                usr.UZT_EMAIL as Email,
                usr.UZT_WBP_ID as WbpId,
                usr.UZT_PODPIS as Signature,
                usr.UZT_FNK_ID as FnkId,
                usr.UZT_SKC_ID as SkcId,
                usr.UZT_PODPIS_ROZSZ as SignatureExtension,
                usr.UZT_NUMER_TELEFONU as Phone
                from wrk_uzytkownicy usr
                where usr.uzt_id = :Id";
                var result = await conn.QueryAsync<User> (query, new { Id = usrId });
                return result.FirstOrDefault ();
            }
        }

        public async Task<List<User>> GetUsersList () {
            using (var conn = Connection) {
                conn.Open ();
                var query = @"select
                usr.uzt_id as Id,
                usr.UZT_IMIE as FirstName,
                usr.UZT_NAZWISKO as LastName,
                usr.UZT_STANOWISKO as Position,
                usr.UZT_LOGIN as Login,
                usr.UZT_EMAIL as Email,
                usr.UZT_WBP_ID as WbpId,
                usr.UZT_PODPIS as Signature,
                usr.UZT_FNK_ID as FnkId,
                usr.UZT_SKC_ID as SkcId,
                usr.UZT_PODPIS_ROZSZ as SignatureExtension,
                usr.UZT_NUMER_TELEFONU as Phone
                from wrk_uzytkownicy usr
                where usr.UZT_AKTYWNE_DO >= sysdate";
                var result = await conn.QueryAsync<User> (query);                
                return result.ToList();
            }
        }
    }
}