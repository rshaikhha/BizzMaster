import { count } from "console";
import { useEffect, useState } from "react";
import agent from "../../app/api/agent";
import { useAppDispatch, useAppSelector } from "../../app/store/configureStore";
import SimpleTable from "../shared/SimpleTable";
export default function Countries() {


    const [list, setList] = useState<any[]>([]);

    useEffect(() => {
        agent.basics.countries().then((res) => setList(res))
    }, [])

    const title = 'Countries';
    const columns = [
        { header: 'Name', accessor: 'title'},
        { header: 'Country', accessor: 'countryName'}

    ];

    

    const props = {list, title, columns}
    return (
        <SimpleTable {...props} />

    )


}