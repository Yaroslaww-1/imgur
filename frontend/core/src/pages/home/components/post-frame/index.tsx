import React from "react";
import { useHistory } from "react-router-dom";

import { IPost } from "@models/post.model";

import styles from "./styles.module.scss";

interface IProps {
  post: IPost;
}

export const PostFrame: React.FC<IProps> = ({ post }) => {
  const history = useHistory();

  function redirect() {
    history.push("/posts/" + post.id);
  }

  return (
    <div className={styles.postFrame} onClick={redirect}>
      <div className={styles.name}>{post.name}</div>
      <div className={styles.createdBy}>Created by: {post.createdBy.name}</div>
      {post.imagesUrls && post.imagesUrls[0] !== null ? (
        <div className={styles.imageWrapper}>
          <img src={post.imagesUrls[0]} className={styles.image} />
        </div>
      ) : (
        <div className={styles.content}>
          {post.content.length > 400
            ? post.content.substring(0, 400) + "..."
            : post.content}
        </div>
      )}
    </div>
  );
};
