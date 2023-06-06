﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TiendaServicios.api.Libro.Tests
{
    public class AsymcEmumerator<T> : IAsyncEnumerator<T>
    {
        private readonly IEnumerator<T> _enumerator;
        public T Current => _enumerator.Current;

        public AsymcEmumerator(IEnumerator<T> enumerator) => this._enumerator = enumerator ?? throw new ArgumentNullException();
        public async ValueTask DisposeAsync()
        {
            await Task.CompletedTask;
        }

        public async ValueTask<bool> MoveNextAsync()
        {
            return await Task.FromResult(_enumerator.MoveNext());
        }
    }
}
