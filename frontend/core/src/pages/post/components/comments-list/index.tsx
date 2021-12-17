import React from "react";

import { IComment } from "@models/comment.model";

import { CommentComponent } from "../comment";

import styles from "./styles.module.scss";

interface IProps {
  postId: string;
  comments: IComment[];
}

export const CommentsListComponent: React.FC<IProps> = ({
  postId,
  comments,
}) => {
  return (
    <div className={styles.commentsList}>
      {comments.map(comment => (
        <CommentComponent key={comment.id} comment={comment} postId={postId} />
      ))}
    </div>
  );
};
