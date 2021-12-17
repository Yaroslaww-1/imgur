import React from "react";

import { PostsService } from "@api/services/posts.service";

import { IComment } from "@models/comment.model";
import { LikeDislikeComponent } from "../like-dislike";

import styles from "./styles.module.scss";

interface IProps {
  postId: string;
  comment: IComment;
}

export const CommentComponent: React.FC<IProps> = ({ postId, comment }) => {
  function toggleLike() {
    PostsService.toggleCommentLike(postId, comment.id);
  }

  function toggleDislike() {
    PostsService.toggleCommentDislike(postId, comment.id);
  }

  return (
    <div className={styles.comment}>
      <div className={styles.user}>{comment.createdBy?.name}</div>
      <div className={styles.content}>{comment.content}</div>
      {comment.authenticatedUserReaction === null ? (
        <LikeDislikeComponent
          likes={comment.likesCount}
          dislikes={comment.dislikesCount}
          isLiked={false}
          isDisliked={false}
          onLike={toggleLike}
          onDislike={toggleDislike}
        />
      ) : (
        <LikeDislikeComponent
          likes={comment.likesCount}
          dislikes={comment.dislikesCount}
          isLiked={comment.authenticatedUserReaction?.isLike}
          isDisliked={!comment.authenticatedUserReaction?.isLike}
          onLike={toggleLike}
          onDislike={toggleDislike}
        />
      )}
    </div>
  );
};
