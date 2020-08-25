using System.Collections.Generic;

namespace NRestGen.TextTemplate
{
    internal static class ResourceObjectModelExtensions
    {
        public static ResourceObjectModel BuildRelations(this ResourceObjectModel model)
        {
            var relations = new Dictionary<string, List<ResourceEntity>>();

            foreach (var entity in model.Entities)
            {
                AddRelations(relations, entity, model.Entities);
            }

            model.Relations = relations;
            return model;
        }

        private static void AddRelations(
            Dictionary<string, List<ResourceEntity>> relations,
            ResourceEntity srcEntity, IEnumerable<ResourceEntity> entities)
        {
            foreach (var prop in srcEntity.Properties)
            {
                if (NameIsEntityReference(srcEntity, prop.Name))
                    continue;

                foreach (var entity in entities)
                {
                    if (TypeIsEntityReference(entity, prop.Type))
                    {
                        AddRelation(relations, srcEntity, entity);
                    }

                    //if (NameIsEntityReference(entity, prop.Name))
                    //{
                    //    AddRelation(relations, entity, srcEntity);
                    //}
                }
            }
        }

        private static void AddRelation(
            Dictionary<string, List<ResourceEntity>> relations,
            ResourceEntity parent, ResourceEntity child)
        {
            var key = parent.Name;

            if (!relations.ContainsKey(key))
            {
                relations.Add(key, new List<ResourceEntity>());
            }

            var relList = relations[key];
            relList.Add(child);
        }

        private static bool TypeIsEntityReference(ResourceEntity entity, string type)
        {
            // IEnumerable<EntityT>
            // List<EntityT>
            return type.Contains(entity.Name);
        }

        private static bool NameIsEntityReference(ResourceEntity entity, string name)
        {
            return name == $"{entity.Name}Id";
        }
    }
}
