using System;
using Web.Interfaces;

namespace Web.ViewModels.Positions
{
    public class PositionUpdateVModel : IUpdateViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}