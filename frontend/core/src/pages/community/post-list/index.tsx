import React, { useEffect } from "react";
import { useParams } from "react-router-dom";
import { observer } from "mobx-react-lite";

import { CommunityState } from "pages/community/community.state";

import { PostFrame } from "pages/home/components/post-frame";

import styles from "./styles.module.scss";

interface IProps {
  state: CommunityState;
}

interface IUrlParams {
  communityId: string;
}

export const PostsList: React.FC<IProps> = observer(({ state }) => {
  const { communityId } = useParams<IUrlParams>();

  useEffect(() => {
    state.fetchPosts(communityId);
  }, []);

  return (
    <div className={styles.list}>
      {state.posts.map(post => (
        <PostFrame key={post.id} post={post} />
      ))}
    </div>
  );
});
