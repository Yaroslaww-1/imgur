import { useEffect } from "react";
import { useParams } from "react-router-dom";
import { observer } from "mobx-react-lite";

import { PostState } from "../post.state";

import { PostComponent } from "../components/post";

interface IProps {
  state: PostState;
}

interface IUrlParams {
  postId: string;
  communityId: string;
}

export const PostContainer: React.FC<IProps> = observer(({ state }) => {
  const { postId } = useParams<IUrlParams>();

  useEffect(() => {
    state.fetchPostById(postId);
  }, []);

  return <PostComponent post={state.post} />;
});
