import { Route, Switch } from "react-router-dom";
import AbsentPage from "../pages/AbsentPage";
const absentRoute = (props) => {
  return (
    <>
      <Switch>
        <>
          <Route path="*" exact component={AbsentPage} />
        </>
      </Switch>
    </>
  );
};
export default absentRoute;
