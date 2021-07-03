using System;
using System.Threading.Tasks;
using ProEventos.Application.Contratos;
using ProEventos.Domain;
using ProEventos.Persistence;

namespace ProEventos.Application
{
    public class EventoService : IEventoService
    {
        private readonly IGeralPersist _geralPersist;
        private readonly IEventoPersist _eventoPersist;
        public EventoService(IGeralPersist geralPersist, IEventoPersist eventoPersist)
        {
            _eventoPersist = eventoPersist;
            _geralPersist = geralPersist;

        }
        public async Task<Evento> AddEventos(Evento model)
        {
            try
            {
                //adiciono a model do método para o geralPersist
                _geralPersist.Add<Evento>(model);

                //se a model for salva
                if (await _geralPersist.SaveChangesAsync())
                {
                    //retornar o evento pelo id da model
                    return (await _eventoPersist.GetEventoByIdAsync(model.Id, false));
                }
                //se não, retorna nulo
                return (null);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Evento> UpdateEvento(int eventoId, Evento model)
        {
            try
            {
                var evento = await _eventoPersist.GetEventoByIdAsync(eventoId, false);
                if (evento == null)
                    return (null);

                //se alguém for encontrado...
                model.Id = eventoId;

                _geralPersist.Update(model);
                if (await _geralPersist.SaveChangesAsync())
                {
                    //retornar o evento pelo id da model
                    return (await _eventoPersist.GetEventoByIdAsync(model.Id, false));
                }
                return (null);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> DeleteEvento(int eventoId)
        {
            try
            {
                var evento = await _eventoPersist.GetEventoByIdAsync(eventoId, false);
                if (evento == null)
                    throw new Exception("Evento de delete não encontrado.");

                _geralPersist.Delete<Evento>(evento);
                return (await _geralPersist.SaveChangesAsync());
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Evento[]> GetAllEventosAsync(bool includePalestrantes = false)
        {
            try
            {
                var eventos = await _eventoPersist.GetAllEventosAsync(includePalestrantes);
                if (eventos == null)
                    return (null);

                return (eventos);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Evento[]> GetAllEventosByTemaAsync(string tema, bool includePalestrantes = false)
        {
            try
            {
                var eventos = await _eventoPersist.GetAllEventosByTemaAsync(tema, includePalestrantes);
                if (eventos == null)
                    return (null);

                return (eventos);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Evento> GetEventoByIdAsync(int eventoId, bool includePalestrantes = false)
        {
            try
            {
                var eventos = await _eventoPersist.GetEventoByIdAsync(eventoId, includePalestrantes);
                if (eventos == null)
                    return (null);

                return (eventos);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Task<Evento> UpdateEvento(int eventoId)
        {
            throw new NotImplementedException();
        }
    }
}