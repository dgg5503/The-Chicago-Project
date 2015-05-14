using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace TheChicagoProject.Entity
{
    class EntityItem : Entity
    {
        private Item.Item item;

        public EntityItem(FloatRectangle rect, Item.Item item) :
            base(rect, item.previewSprite) {
                this.item = item;
        }

        public override bool Action(LivingEntity interactor, String interaction) {
            if (item == null || !base.Action(interactor, interaction))
                return false;
            interactor.inventory.Add(item);
            this.item = null;
            this.markForDelete = true;
            return true;
        }
    }
}
