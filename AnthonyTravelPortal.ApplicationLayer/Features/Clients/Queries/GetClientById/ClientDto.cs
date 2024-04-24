﻿using AnthonyTravelPortal.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnthonyTravelPortal.ApplicationLayer.Features.Clients.Queries.GetClientById
{
    public class ClientDto
    {
        public string Client_ID { get; set; }
        public string? Client_Name { get; set; }
        public int? Institution_ID { get; set; }

    }
}
