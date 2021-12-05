import React from "react";

import { IPost } from "@models/post.model";

import styles from "./styles.module.scss";

interface IProps {
  post: IPost;
}

export const PostComponent: React.FC<IProps> = props => {
  return (
    <div className={styles.contentWrapper}>
      <div className={styles.name}>
        <h1>{props.post.name}</h1>
      </div>
      <div className={styles.imageWrapper}>
        <i className={"fa fa-image fa-5x"}></i>
      </div>
      <div className={styles.content}>{props.post.content}</div>

      <div className={styles.likesCount}>{props.post.likesCount}</div>
      <div className={styles.dislikesCount}>{props.post.dislikesCount}</div>
      <div className={styles.commentsCount}>
        Comments: {props.post.commentsCount}
      </div>
    </div>
  );
};
