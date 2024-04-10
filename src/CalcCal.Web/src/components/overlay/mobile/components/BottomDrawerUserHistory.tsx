import {useStore} from "../../../../stores/store";
import {observer} from "mobx-react-lite";

const BottomDrawerUserHistory = () => {
    const {userStore} = useStore()

    return (
        <>
            Here goes the history chart
        </>
    );
};

export default observer(BottomDrawerUserHistory)