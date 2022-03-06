import { useEffect, useState } from "react";
import agent from "../../app/api/agent";
import SimpleTable from "../shared/SimpleTable";
export default function Platforms() {


    const [list, setList] = useState<any[]>([]);

    useEffect(() => {
        agent.Cars.platforms().then((res) => setList(res))
    }, [])

    const title = 'Car Platforms';
    const columns = [
        { header: 'Name', accessor: 'title'},
        { header: 'Brand', accessor: 'brandTitle'},
        { header: 'Country', accessor: 'countryName'}

    ];



    const props = {list, title, columns}
    return (
        <SimpleTable {...props} />

    )


}