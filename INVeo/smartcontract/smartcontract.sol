// SPDX-License-Identifier: MIT
pragma solidity ^0.8;

contract InvestmentIdeasMarketplace {

    address public owner;
    mapping(address => bool) public authenticatedUsers; // Track authenticated users
    mapping(uint256 => Idea) public ideas;
    uint256 public ideaCount;

    struct Idea {
        uint256 id;
        address payable seller;
        string title;
        string description;
        uint256 price;
        bool isAvailable;
    }

    event IdeaCreated(uint256 id, address seller, string title, uint256 price);
    event IdeaPurchased(uint256 id, address buyer, string title, uint256 price);

    modifier onlyAuthenticated() {
        require(authenticatedUsers[msg.sender], "User not authenticated");
        _;
    }

    modifier onlyOwner() {
        require(msg.sender == owner, "Only the owner can call this function");
        _;
    }

    constructor() {
        owner = msg.sender;
        authenticatedUsers[msg.sender] = true; // Owner is automatically authenticated
    }

    function authenticateUser() external {
        authenticatedUsers[msg.sender] = true;
    }

    function createIdea(string memory _title, string memory _description, uint256 _price) external onlyAuthenticated {
        require(bytes(_title).length > 0, "Title cannot be empty");
        require(bytes(_description).length > 0, "Description cannot be empty");
        require(_price > 0, "Price must be greater than 0");

        ideaCount++;

        Idea memory newIdea = Idea({
            id: ideaCount,
            seller: payable(msg.sender),
            title: _title,
            description: _description,
            price: _price,
            isAvailable: true
        });

        ideas[ideaCount] = newIdea;
        emit IdeaCreated(ideaCount, msg.sender, _title, _price);
    }

    function purchaseIdea(uint256 _id) external payable onlyAuthenticated {
        Idea storage idea = ideas[_id];
        require(idea.isAvailable, "Idea is not available");
        require(msg.value >= idea.price, "Insufficient funds");

        idea.seller.transfer(msg.value);
        idea.isAvailable = false;

        emit IdeaPurchased(_id, msg.sender, idea.title, idea.price);
    }
}