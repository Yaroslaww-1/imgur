import React, { useState } from "react";

import { PostsService } from "@api/services/posts.service";
import { IComment } from "@models/comment.model";

import { CommentsListComponent } from "../comments-list";
import { Input } from "@components/input";
import { SimpleButton } from "@components/buttons/simple-button";

import styles from "./styles.module.scss";

interface IProps {
  postId: string;
  comments: IComment[];
  updateComments: () => void;
}

export const CommentsSectionComponent: React.FC<IProps> = props => {
  const [newComment, setNewComment] = useState("");

  function onSubmit(e: React.FormEvent<HTMLFormElement>) {
    e.preventDefault();
    PostsService.createPostComment(props.postId, newComment).then(() => {
      props.updateComments();
    });
  }

  return (
    <div className={styles.section}>
      <h2>Comments</h2>
      <form className={styles.form} onSubmit={onSubmit}>
        <Input
          text={""}
          placeholder={"Add comment:"}
          required
          onChange={(e: React.ChangeEvent<HTMLInputElement>) => {
            setNewComment(e.target.value);
          }}
        />
        <SimpleButton text={"Comment"} size={"small"} type={"submit"} />
      </form>
      <CommentsListComponent postId={props.postId} comments={props.comments} />
    </div>
  );
};
