import { Route, Switch } from "react-router-dom";
import HouseholdPage from "../pages/HouseholdPage";
const householdRoute = (props) => {
  return (
    <>
      <Switch>
        <>
          <Route path="*" exact component={HouseholdPage} />
        </>
      </Switch>
    </>
  );
};
export default householdRoute;
