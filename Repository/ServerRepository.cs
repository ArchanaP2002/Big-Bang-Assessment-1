using HotelApi.data;
using HotelApi.model_s;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HotelApi.Repository
{
    public class ServerRepository : IServer
    {
        private readonly HotelContext _context;

        public ServerRepository(HotelContext context)
        {
            _context = context;
        }

        public IEnumerable<ServersDetails> GetServers()
        {
            try
            {
                return _context.Servers.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to retrieve servers.", ex);
            }
        }

        public ServersDetails GetServerById(int id)
        {
            try
            {
                return _context.Servers.Find(id);
            }
            catch (Exception ex)
            {
                throw new Exception($"Server not found with ID: {id}", ex);
            }
        }

        public ServersDetails PostServer(ServersDetails server)
        {
            try
            {
                _context.Add(server);
                _context.SaveChanges();
                return server;
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to create server.", ex);
            }
        }

        public ServersDetails PutServer(int id, ServersDetails server)
        {
            try
            {
                if (server == null || server.EmployeeId != id)
                {
                    throw new ArgumentException("Invalid data or ID.");
                }

                var emp = _context.Servers.Find(id);
                if (emp == null)
                {
                    throw new Exception($"Server not found with ID: {id}");
                }

                emp.EmployeeName = server.EmployeeName;

                _context.SaveChanges();

                return emp;
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to update server.", ex);
            }
        }

        public void DeleteServer(int id)
        {
            try
            {
                var emp = _context.Servers.Find(id);
                if (emp == null)
                {
                    throw new Exception($"Server not found with ID: {id}");
                }

                _context.Servers.Remove(emp);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to delete server.", ex);
            }
        }
    }
}

