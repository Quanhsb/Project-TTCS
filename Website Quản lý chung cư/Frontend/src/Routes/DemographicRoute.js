import { Route, Switch } from "react-router-dom";
import DemographicPage from "../pages/DemographicPage";
const demographicRoute = (props) => {
  return (
    <>
      <Switch>
        <>
          <Route path="*" exact component={DemographicPage} />
        </>
      </Switch>
    </>
  );
};
export default demographicRoute;
