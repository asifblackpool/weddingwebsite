﻿using RazorPageWeddingWebsite.Constants;
using RazorPageWeddingWebsite.Models;
using Blackpool.Zengenti.CMS.Models.Weddings;
using RazorPageWeddingWebsite.Services.Interfaces;
using Zengenti.Contensis.Delivery;

namespace RazorPageWeddingWebsite.Services
{
    public class ContensisDataService<T> : IDataService<T> where T : class, new()
    {
        private List<T> _data;
        private string  _path       = WebsiteConstants.SITE_VIEW_PATH;
        private bool    _dataLoaded = false;

        // Proper constructor with no return type
        public ContensisDataService()
        {
            _data = new List<T>();
        }


        private void LoadData() {

            var client = ContensisClient.Create();

            // Get our blog node and its children

            var node = client.Nodes.GetByPath(_path, null, 1);

            var entryId = (node != null) ? node.EntryId : null;
            if (entryId != null)
            {
                var entry = client.Entries.Get<T>((Guid)entryId, null, 1);
                _data.Add(entry);

            }

            _dataLoaded = true;
        }

        private void CheckData(string? path, bool ignore = true)
        {
            if (path != null && _path != path)
            {
                _path = (ignore) ? path : _path + path;
            }

            if( !_dataLoaded)
                LoadData();
          
        }

        public Task<List<T>> GetAllAsync(string? path)
        {
            CheckData(path);
            return Task.FromResult(_data);
        }

        public Task<T?> GetByIdAsync(int id, string? path)
        {

            var item = _data.FirstOrDefault(x =>
                (int)x.GetType().GetProperty("Id")!.GetValue(x)! == id);
            return Task.FromResult(item);
        }
    }
}
