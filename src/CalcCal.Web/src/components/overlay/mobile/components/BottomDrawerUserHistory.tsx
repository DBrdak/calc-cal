import {useStore} from "../../../../stores/store";
import {observer} from "mobx-react-lite";
import UserHistoryChart from "../../../../app/user/components/shared/UserHistoryChart";

const BottomDrawerUserHistory = () => {
    const {userStore} = useStore()

    return (
        <>
            <UserHistoryChart mobile />
        </>
    );
};

export default observer(BottomDrawerUserHistory)