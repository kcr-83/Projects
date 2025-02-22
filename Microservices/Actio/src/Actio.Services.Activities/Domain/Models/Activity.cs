using System;

namespace Actio.Services.Activities.Domain.Models
{
    public class Activity
    {
         public Activity()
        {
        }

        public Activity(Guid id, Category cateogry, Guid userId, string name, string description, DateTime createdAt)
        {
            Id = id;
            Category = cateogry.Name;
            UserId = userId;
            Name = name;
            Description = description;
            CreatedAt = createdAt;
        }

        public Guid Id { get; protected set; }
        public string Name { get; protected set; }
        
        public string Category { get; protected set; }
        public string Description { get; protected set; }
        public Guid UserId { get; protected set; }
        public DateTime CreatedAt { get; protected set; }
    }
}