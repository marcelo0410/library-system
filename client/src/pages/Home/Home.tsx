import React, { useEffect, useState } from 'react'
import { Table, Space, Input } from 'antd';
import { AudioOutlined } from '@ant-design/icons';

import type { ColumnsType, TableProps } from 'antd/es/table';
import { fetchBooks } from '../../api/index';
import { Book } from '../../common/interface';


const columns: ColumnsType<Book> = [
  {
    title: 'Title',
    dataIndex: 'title',
    width: '15%',
  },
  {
    title: 'publisher',
    dataIndex: 'publisher',
    width: '15%',
  },
  {
    title: 'Published At',
    dataIndex: 'dateOfPublication',
    render: ((date:string) => getFullDate(date)),
    width: '15%',
  },
  {
    title: 'Status',
    dataIndex: 'isBorrowed',
    render: (text, record) => {
        return {
            props: {
                style: {color: !record.isBorrowed ? "green" : "red" }
            },
            children: <div>{!text? 'Available':'Unavailable'}</div>
        }
    },
    width: '15%',
  },
  {
    title: '',
    dataIndex: 'isBorrowed',
    render: (_, record) => (
      <Space size="middle" >
        {!record.isBorrowed? <a style={{color:"orange"}}>Borrow</a>: <></>}
      </Space>
    ),
    width: '20%',
  }
];

const getFullDate = (date: string): string => {
    const dateAndTime = date.split('T');

    return dateAndTime[0].split('-').reverse().join('-');
};

const { Search } = Input;

const suffix = (
  <AudioOutlined
    style={{
      fontSize: 16,
      color: '#1890ff',
    }}
  />
);




const onChange: TableProps<Book>['onChange'] = (pagination, filters, sorter, extra) => {
  console.log('params', pagination, filters, sorter, extra);
};

const Home: React.FC = () => {

    const [bookData, setBookData] = useState<Book[]>([]);
    const [filteredBooks, setFilteredBooks] = useState<Book[]>([]);
    const [value, setValue] = useState<string>('');

    const onChangeInput = (event: React.ChangeEvent<HTMLInputElement>) => {
        const filteredData = bookData.filter(entry =>
            entry.title.toLowerCase().includes(event.target.value.toLowerCase())
        );
        setFilteredBooks(filteredData);
    }

    useEffect(() => {
        fetchBooks().then(data => {setBookData(data.data); setFilteredBooks(data.data);});
    }, [])

    return (
        <div>
            <Space direction='vertical'>
                <Input
                    placeholder="input search text"
                    size="large"
                    suffix={suffix}
                    onChange={(event) => onChangeInput(event)}
                />
            </Space>
            <Table columns={columns} dataSource={filteredBooks} onChange={onChange} />
        </div>
        
    )
}


export default Home;