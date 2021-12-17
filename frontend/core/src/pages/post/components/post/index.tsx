import React from "react";

import { PostsService } from "@api/services/posts.service";
import { IPost } from "@models/post.model";

import { LikeDislikeComponent } from "../like-dislike";

import styles from "./styles.module.scss";

interface IProps {
  post: IPost;
}

export const PostComponent: React.FC<IProps> = props => {
  function toggleLike() {
    PostsService.togglePostLike(props.post.id);
  }

  function toggleDislike() {
    PostsService.togglePostDislike(props.post.id);
  }

  return (
    <div className={styles.contentWrapper}>
      <div className={styles.name}>
        <h1>{props.post.name}</h1>
      </div>
      <div className={styles.imageWrapper}>
        <i className={"fa fa-image fa-5x"}></i>
      </div>
      <div className={styles.content}>{props.post.content}</div>
      {props.post.authenticatedUserReaction === null ? (
        <LikeDislikeComponent
          likes={props.post.likesCount}
          dislikes={props.post.dislikesCount}
          isLiked={false}
          isDisliked={false}
          onLike={toggleLike}
          onDislike={toggleDislike}
        />
      ) : (
        <LikeDislikeComponent
          likes={props.post.likesCount}
          dislikes={props.post.dislikesCount}
          isLiked={props.post.authenticatedUserReaction?.isLike}
          isDisliked={!props.post.authenticatedUserReaction?.isLike}
          onLike={toggleLike}
          onDislike={toggleDislike}
        />
      )}
      <div className={styles.commentsCount}>
        Comments: {props.post.commentsCount}
      </div>
    </div>
  );
};
