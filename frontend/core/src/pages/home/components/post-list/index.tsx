import React, { useEffect } from "react";
import { observer } from "mobx-react-lite";

import { PostsState } from "pages/home/posts.state";

import { PostFrame } from "../post-frame";

import styles from "./styles.module.scss";

interface IProps {
  state: PostsState;
}

export const PostsList: React.FC<IProps> = observer(({ state }) => {
  useEffect(() => {
    state.fetchPosts();
  }, []);

  return (
    <div className={styles.list}>
      {state.posts.map(post => (
        <PostFrame key={post.id} post={post} />
      ))}
    </div>
  );
});
