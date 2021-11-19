import { Route, Switch } from "react-router-dom";

import { CreatePost } from "pages/create-post";
import { AppRoute } from "@common/enums/app-route.enum";

export const App = () => {
  return (
    <>
      <Switch>
        <Route path={AppRoute.CREATE_POST} component={CreatePost} />
      </Switch>
    </>
  );
};
