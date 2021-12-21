import { Redirect, Route, Switch } from "react-router-dom";
import { useContext, useEffect, useState } from "react";
import { observer } from "mobx-react-lite";

import { AppRoute } from "@common/enums/app-route.enum";

import { Context } from "index";

import { Login } from "pages/login";
import { Signup } from "pages/signup";
import { Home } from "pages/home";
import { CreatePost } from "pages/create-post";
import { Post } from "pages/post";
import { UserProfile } from "pages/user-profile";

import { Header } from "@components/header";
import { PrivateRoute } from "@components/private-route";
import { Loader } from "@components/loader";

export const App = observer(() => {
  const { store: authStore } = useContext(Context);
  const [authenticating, setAuthenticating] = useState<boolean>(true);

  useEffect(() => {
    (async () => {
      setAuthenticating(true);
      await authStore.checkAuth();
      setAuthenticating(false);
    })();
  }, []);

  return !authenticating ? (
    <>
      <Header />
      <Switch>
        <Route path={AppRoute.LOGIN} component={Login} />
        <Route path={AppRoute.SIGNUP} component={Signup} />
        <PrivateRoute path={AppRoute.HOME} component={Home} />
        <PrivateRoute path={AppRoute.CREATE_POST} component={CreatePost} />
        <PrivateRoute path={AppRoute.POST} component={Post} />
        <PrivateRoute path={AppRoute.USER_PROFILE} component={UserProfile} />
        <Redirect to={AppRoute.HOME} />
      </Switch>
    </>
  ) : (
    <Loader />
  );
});
