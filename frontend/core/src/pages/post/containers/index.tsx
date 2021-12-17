import { useEffect } from "react";
import { Redirect, useParams } from "react-router-dom";
import { observer } from "mobx-react-lite";

import { PostState } from "../post.state";
import { AppRoute } from "@common/enums/app-route.enum";
import { FetchStatus } from "@common/enums/fetch-status.enum";

import { PostComponent } from "../components/post";
import { Loader } from "@components/loader";

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
    state.fetchPostComments(postId);
  }, []);

  function updateComments() {
    state.fetchPostComments(postId);
  }

  return state.state === FetchStatus.ERROR ? (
    <Redirect to={AppRoute.HOME} />
  ) : state.state === FetchStatus.PENDING ? (
    <Loader />
  ) : (
    <PostComponent
      post={state.post}
      comments={state.comments}
      updateComments={updateComments}
    />
  );
});
