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
  const { communityId } = useParams<IUrlParams>();

  useEffect(() => {
    state.fetchPostById(communityId, postId);
  }, []);

  // return <PostComponent post={state.post} />;
  return (
    <PostComponent
      post={{
        id: "2227f6d8-454b-477d-945f-6c048ab82f80",
        name: "C# 9.0 is released!",
        content:
          "A lot of new features. Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum.",
        commentsCount: 0,
        likesCount: 0,
        dislikesCount: 0,
      }}
    />
  );
});
