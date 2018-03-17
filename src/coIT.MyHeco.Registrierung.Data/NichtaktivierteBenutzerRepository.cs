using System.Collections.Generic;
using System.Linq;
using coIT.MyHeco.Registrierung.Domain;
using coIT.MyHeco.Registrierung.Domain.Services;
using MediatR;

namespace coIT.MyHeco.Registrierung.Data
{
    public class NichtaktivierteBenutzerRepository : INichtAktivierteBenutzerRepository
    {
        private static IDictionary<string, Benutzer> _users;
        private readonly IMediator _mediator;

        public NichtaktivierteBenutzerRepository(IMediator mediator)
        {
            _mediator = mediator;
            if (_users == null)
                _users = new Dictionary<string, Benutzer>();
        }

        public Benutzer FindeBenutzerByMail(string email)
        {
            if (_users.ContainsKey(email))
                return _users[email];
            return null;
        }

        public IEnumerable<Benutzer> All()
        {
            return _users.Values;
        }

        public void Add(NichtAktivierterBenutzer benutzer)
        {
            _users.Add(benutzer.Email, benutzer);
        }

        public void Delete(NichtAktivierterBenutzer benutzer)
        {
            _users.Remove(benutzer.Email);
        }

        public void Speichern()
        {
            foreach (var user in _users.Values)
            {
                user.DomainEvents.ToList().ForEach(foo => _mediator.Publish(foo));
                user.ClearEvents();
            }
        }
    }
}