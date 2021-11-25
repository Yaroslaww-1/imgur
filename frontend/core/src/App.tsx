import { Route, Switch } from "react-router-dom";

import { AppRoute } from "@common/enums/app-route.enum";

import { CreatePost } from "pages/create-post";
import { Login } from "pages/login";

export const App = () => {
  return (
    <>
      <Switch>
        <Route path={AppRoute.CREATE_POST} component={CreatePost} />
        <Route path={AppRoute.LOGIN} component={Login} />
      </Switch>
    </>
  );
};
