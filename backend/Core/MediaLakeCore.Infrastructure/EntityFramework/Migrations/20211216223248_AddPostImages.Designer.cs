﻿// <auto-generated />
using System;
using MediaLakeCore.Infrastructure.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace MediaLakeCore.Infrastructure.EntityFramework.Migrations
{
    [DbContext(typeof(MediaLakeCoreDbContext))]
    [Migration("20211216223248_AddPostImages")]
    partial class AddPostImages
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.11")
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            modelBuilder.Entity("MediaLakeCore.Domain.CommentReactions.CommentReaction", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<Guid>("CommentId")
                        .HasColumnType("uuid")
                        .HasColumnName("comment_id");

                    b.Property<Guid>("CreatedBy")
                        .HasColumnType("uuid")
                        .HasColumnName("created_by");

                    b.Property<bool>("IsLike")
                        .HasColumnType("boolean")
                        .HasColumnName("is_like");

                    b.HasKey("Id")
                        .HasName("pk_comment_reaction");

                    b.HasIndex("CommentId")
                        .HasDatabaseName("ix_comment_reaction_comment_id");

                    b.ToTable("comment_reaction");
                });

            modelBuilder.Entity("MediaLakeCore.Domain.Comments.Comment", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("content");

                    b.Property<Guid?>("CreatedById")
                        .HasColumnType("uuid")
                        .HasColumnName("created_by_id");

                    b.Property<int>("DislikesCount")
                        .HasColumnType("integer")
                        .HasColumnName("dislikes_count");

                    b.Property<int>("LikesCount")
                        .HasColumnType("integer")
                        .HasColumnName("likes_count");

                    b.Property<Guid>("PostId")
                        .HasColumnType("uuid")
                        .HasColumnName("post_id");

                    b.HasKey("Id")
                        .HasName("pk_comment");

                    b.HasIndex("CreatedById")
                        .HasDatabaseName("ix_comment_created_by_id");

                    b.ToTable("comment");
                });

            modelBuilder.Entity("MediaLakeCore.Domain.Communities.Community", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<Guid?>("CreatedById")
                        .HasColumnType("uuid")
                        .HasColumnName("created_by_id");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("description");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.HasKey("Id")
                        .HasName("pk_community");

                    b.HasIndex("CreatedById")
                        .HasDatabaseName("ix_community_created_by_id");

                    b.ToTable("community");
                });

            modelBuilder.Entity("MediaLakeCore.Domain.CommunityMember.CommunityMember", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<Guid?>("CommunityId")
                        .HasColumnType("uuid")
                        .HasColumnName("community_id");

                    b.Property<Guid?>("UserId")
                        .HasColumnType("uuid")
                        .HasColumnName("user_id");

                    b.HasKey("Id")
                        .HasName("pk_community_member");

                    b.HasIndex("CommunityId")
                        .HasDatabaseName("ix_community_member_community_id");

                    b.HasIndex("UserId")
                        .HasDatabaseName("ix_community_member_user_id");

                    b.ToTable("community_member");
                });

            modelBuilder.Entity("MediaLakeCore.Domain.PostImages.PostImage", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<Guid?>("PostId")
                        .HasColumnType("uuid")
                        .HasColumnName("post_id");

                    b.Property<string>("Url")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("url");

                    b.HasKey("Id")
                        .HasName("pk_post_image");

                    b.HasIndex("PostId")
                        .HasDatabaseName("ix_post_image_post_id");

                    b.ToTable("post_image");
                });

            modelBuilder.Entity("MediaLakeCore.Domain.PostReactions.PostReaction", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<Guid>("CreatedBy")
                        .HasColumnType("uuid")
                        .HasColumnName("created_by");

                    b.Property<bool>("IsLike")
                        .HasColumnType("boolean")
                        .HasColumnName("is_like");

                    b.Property<Guid>("PostId")
                        .HasColumnType("uuid")
                        .HasColumnName("post_id");

                    b.HasKey("Id")
                        .HasName("pk_post_reaction");

                    b.HasIndex("PostId")
                        .HasDatabaseName("ix_post_reaction_post_id");

                    b.ToTable("post_reaction");
                });

            modelBuilder.Entity("MediaLakeCore.Domain.Posts.Post", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<int>("CommentsCount")
                        .HasColumnType("integer")
                        .HasColumnName("comments_count");

                    b.Property<Guid>("CommunityId")
                        .HasColumnType("uuid")
                        .HasColumnName("community_id");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("content");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("created_at");

                    b.Property<Guid?>("CreatedById")
                        .HasColumnType("uuid")
                        .HasColumnName("created_by_id");

                    b.Property<int>("DislikesCount")
                        .HasColumnType("integer")
                        .HasColumnName("dislikes_count");

                    b.Property<int>("LikesCount")
                        .HasColumnType("integer")
                        .HasColumnName("likes_count");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.HasKey("Id")
                        .HasName("pk_post");

                    b.HasIndex("CommunityId")
                        .HasDatabaseName("ix_post_community_id");

                    b.HasIndex("CreatedById")
                        .HasDatabaseName("ix_post_created_by_id");

                    b.ToTable("post");
                });

            modelBuilder.Entity("MediaLakeCore.Domain.Users.Role", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.HasKey("Id")
                        .HasName("pk_role");

                    b.HasIndex("Name")
                        .IsUnique()
                        .HasDatabaseName("ix_role_name");

                    b.ToTable("role");
                });

            modelBuilder.Entity("MediaLakeCore.Domain.Users.User", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("email");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.HasKey("Id")
                        .HasName("pk_user");

                    b.HasIndex("Email")
                        .IsUnique()
                        .HasDatabaseName("ix_user_email");

                    b.ToTable("user");
                });

            modelBuilder.Entity("user_role", b =>
                {
                    b.Property<Guid>("role_id")
                        .HasColumnType("uuid")
                        .HasColumnName("role_id");

                    b.Property<Guid>("user_id")
                        .HasColumnType("uuid")
                        .HasColumnName("user_id");

                    b.HasKey("role_id", "user_id")
                        .HasName("pk_user_role");

                    b.HasIndex("user_id")
                        .HasDatabaseName("ix_user_role_user_id");

                    b.ToTable("user_role");
                });

            modelBuilder.Entity("MediaLakeCore.Domain.CommentReactions.CommentReaction", b =>
                {
                    b.HasOne("MediaLakeCore.Domain.Comments.Comment", null)
                        .WithMany()
                        .HasForeignKey("CommentId")
                        .HasConstraintName("fk_comment_reaction_comment_comment_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("MediaLakeCore.Domain.Comments.Comment", b =>
                {
                    b.HasOne("MediaLakeCore.Domain.Users.User", "CreatedBy")
                        .WithMany()
                        .HasForeignKey("CreatedById")
                        .HasConstraintName("fk_comment_users_created_by_id");

                    b.Navigation("CreatedBy");
                });

            modelBuilder.Entity("MediaLakeCore.Domain.Communities.Community", b =>
                {
                    b.HasOne("MediaLakeCore.Domain.Users.User", "CreatedBy")
                        .WithMany()
                        .HasForeignKey("CreatedById")
                        .HasConstraintName("fk_community_users_created_by_id");

                    b.Navigation("CreatedBy");
                });

            modelBuilder.Entity("MediaLakeCore.Domain.CommunityMember.CommunityMember", b =>
                {
                    b.HasOne("MediaLakeCore.Domain.Communities.Community", null)
                        .WithMany()
                        .HasForeignKey("CommunityId")
                        .HasConstraintName("fk_community_member_community_community_id");

                    b.HasOne("MediaLakeCore.Domain.Users.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .HasConstraintName("fk_community_member_users_user_id");
                });

            modelBuilder.Entity("MediaLakeCore.Domain.PostImages.PostImage", b =>
                {
                    b.HasOne("MediaLakeCore.Domain.Posts.Post", null)
                        .WithMany()
                        .HasForeignKey("PostId")
                        .HasConstraintName("fk_post_image_post_post_id");

                    b.OwnsOne("MediaLakeCore.Domain.PostImages.PostImageStatus", "Status", b1 =>
                        {
                            b1.Property<Guid>("PostImageId")
                                .HasColumnType("uuid")
                                .HasColumnName("id");

                            b1.Property<string>("Value")
                                .HasColumnType("text")
                                .HasColumnName("status_code");

                            b1.HasKey("PostImageId")
                                .HasName("pk_post_image");

                            b1.ToTable("post_image");

                            b1.WithOwner()
                                .HasForeignKey("PostImageId")
                                .HasConstraintName("fk_post_image_post_image_id");
                        });

                    b.Navigation("Status");
                });

            modelBuilder.Entity("MediaLakeCore.Domain.PostReactions.PostReaction", b =>
                {
                    b.HasOne("MediaLakeCore.Domain.Posts.Post", null)
                        .WithMany()
                        .HasForeignKey("PostId")
                        .HasConstraintName("fk_post_reaction_post_post_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("MediaLakeCore.Domain.Posts.Post", b =>
                {
                    b.HasOne("MediaLakeCore.Domain.Communities.Community", null)
                        .WithMany()
                        .HasForeignKey("CommunityId")
                        .HasConstraintName("fk_post_community_community_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MediaLakeCore.Domain.Users.User", "CreatedBy")
                        .WithMany()
                        .HasForeignKey("CreatedById")
                        .HasConstraintName("fk_post_users_created_by_id");

                    b.Navigation("CreatedBy");
                });

            modelBuilder.Entity("user_role", b =>
                {
                    b.HasOne("MediaLakeCore.Domain.Users.Role", null)
                        .WithMany()
                        .HasForeignKey("role_id")
                        .HasConstraintName("fk_user_role_role_role_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MediaLakeCore.Domain.Users.User", null)
                        .WithMany()
                        .HasForeignKey("user_id")
                        .HasConstraintName("fk_user_role_user_user_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
